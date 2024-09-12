using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using Castle.DynamicProxy;
using Looplex.DotNet.Core.Domain.Traits;
using Looplex.OpenForExtension.Abstractions.Traits;
using Looplex.DotNet.Core.Common.ExtensionMethods;

namespace Looplex.DotNet.Core.Application.ExtensionMethods;

public static class ObservableProxy
{
    private static readonly ProxyGenerator ProxyGenerator = new();

    /// <summary>
    /// This creates a proxy on top of classes that implements IHasChangedPropertyNotificationTrait (they have the
    /// ChangedProperties, AddedItems and RemovedItems) for tracking changes on members.
    /// If a member also implements IHasChangedPropertyNotificationTrait, it will also have a proxy set up for it.
    /// The same for items in collections.
    /// The proxy only will work for virtual properties on the classToProxy.
    /// For enumerable members, only items inside a ObservableCollection instance will have a proxy.
    /// </summary>
    /// <param name="classToProxy">The instance to create the observable proxy for</param>
    /// <typeparam name="T">Type of classToProxy</typeparam>
    /// <returns>An observable proxy for classToProxy</returns>
    /// <exception cref="ArgumentException"></exception>
    public static T WithObservableProxy<T>(this T classToProxy) where T : class, new()
    {
        return (T)WithObservableProxy(typeof(T), classToProxy);
    }

    /// <summary>
    /// This creates a proxy on top of classes that implements IHasChangedPropertyNotificationTrait (they have the
    /// ChangedProperties, AddedItems and RemovedItems) for tracking changes on members.
    /// If a member also implements IHasChangedPropertyNotificationTrait, it will also have a proxy set up for it.
    /// The same for items in collections.
    /// The proxy only will work for virtual properties on the classToProxy.
    /// For enumerable members, only items inside a ObservableCollection instance will have a proxy.
    /// </summary>
    /// <param name="type">Type of classToProxy</param>
    /// <param name="classToProxy">The instance to create the observable proxy for</param>
    /// <returns>An observable proxy for classToProxy</returns>
    /// <exception cref="ArgumentException"></exception>
    private static object WithObservableProxy(Type type, object classToProxy)
    {
        var instance = classToProxy as IHasChangedPropertyNotificationTrait 
                       ?? throw new ArgumentException($"{nameof(classToProxy)} should be instance of {nameof(IHasChangedPropertyNotificationTrait)}");
        
        WithObservableCollectionItemsProxies(type, instance);

        var proxy = ProxyGenerator.CreateClassProxyWithTarget(
            type,
            classToProxy,
            new ProxyGenerationOptions(new GeneralProxyGenerationHook()),
            new NotifyPropertyChangedInterceptor())!;
        
        // bind OnCollectionChanged on all collections that are not readonly
        var setters = type
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.IsSpecialName && m.Name.StartsWith("set_"));
        
        foreach (var setter in setters)
        {
            var propertyName = setter.Name[4..];
            var property = type.GetProperty(propertyName)!;
            
            if (property.GetValue(proxy) is INotifyCollectionChanged collection)
            {
                BindOnCollectionChanged(property.Name, (IHasChangedPropertyNotificationTrait)proxy, collection);
            }
        }

        // make all child members that are observable a proxy
        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => typeof(IHasChangedPropertyNotificationTrait).IsAssignableFrom(p.PropertyType));

        foreach (var property in properties)
        {
            var value = property.GetValue(proxy);
            if (value != default)
                SetChildProxyProperty(
                    proxy, 
                    property,
                    WithObservableProxy(property.PropertyType, value));
        }
        
        return proxy;
    }

    private static void WithObservableCollectionItemsProxies(Type type, IHasChangedPropertyNotificationTrait instance)
    {
        // make all ObservableCollection items that are observable a proxy
        var listProperties = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(p => 
                p.PropertyType.IsNonStringEnumerable()
                && typeof(IHasChangedPropertyNotificationTrait).IsAssignableFrom(p.PropertyType.GetGenericArguments()[0]));

        foreach (var property in listProperties)
        {
            var value = property.GetValue(instance);
            var itemType = property.PropertyType.GetGenericArguments()[0];
            if (value == default || !value.GetType().IsObservableCollection()) break;
            
            var collection = (IList)value;
            
            WithObservableCollectionItemsProxy(collection);
        }
    }

    private static void WithObservableCollectionItemsProxy(IList collection)
    {
        for (var i = 0; i < collection.Count; i++)
        {
            collection[i] = WithObservableProxy(collection[i]!.GetType(), collection[i]!);
        }
    }

    private static void SetChildProxyProperty(object parentProxy, PropertyInfo proxyProperty, object childProxyInstance)
    {
        var interceptors = (IInterceptor[]?)parentProxy.GetType()
            .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            .FirstOrDefault(f => f.FieldType == typeof(IInterceptor[]))
            ?.GetValue(parentProxy);
        var interceptor = (NotifyPropertyChangedInterceptor?)interceptors
            ?.FirstOrDefault(i => i is NotifyPropertyChangedInterceptor);
        
        // Disable interception
        if (interceptor != default) interceptor.InterceptionEnabled = false;

        // Set the property value
        proxyProperty.SetValue(parentProxy, childProxyInstance);

        // Re-enable interception
        if (interceptor != default) interceptor.InterceptionEnabled = true;
    }
    
    private static void OnPropertyChanged(string propertyName, IHasChangedPropertyNotificationTrait target)
    {
        // dispaches OnPropertyChanged event
        if (target is IHasEventHandlerTrait eventHandlerTrait)
            eventHandlerTrait.EventHandling.Invoke(
                target.ChangedPropertyNotification.PropertyChangedEventName, 
                target,
                new PropertyChangedEventArgs(propertyName));
        
        // track the changes
        if (!target.ChangedPropertyNotification.ChangedProperties.Contains(propertyName))
            target.ChangedPropertyNotification.ChangedProperties.Add(propertyName);
    }

    private static void BindOnCollectionChanged(string propertyName, IHasChangedPropertyNotificationTrait target,
        INotifyCollectionChanged collection)
    {
        collection.CollectionChanged += (sender, e) =>
        {
            // dispaches OnPropertyChanged event
            if (target is IHasEventHandlerTrait eventHandlerTrait)
                eventHandlerTrait.EventHandling.Invoke(
                    target.ChangedPropertyNotification.CollectionChangedEventName,
                    target,
                    e);
            
            if (e is { Action: NotifyCollectionChangedAction.Add, NewItems: not null })
            {
                var list = (IList)sender!;

                for (int i = 0; i < e.NewItems.Count; i++)
                {
                    if (e.NewItems[i] == default
                        || !(e.NewItems[i] is IHasChangedPropertyNotificationTrait)) break;
                    
                    // Replace the original item with the proxy in the collection
                    int index = e.NewStartingIndex + i;
                    list[index] = WithObservableProxy(e.NewItems[i]!.GetType(), e.NewItems[i]!);
                }
                
                // track the changes
                if (!target.ChangedPropertyNotification.AddedItems.ContainsKey(propertyName))
                    target.ChangedPropertyNotification.AddedItems.Add(propertyName, new List<object>());
                foreach (var item in e.NewItems)
                {
                    target.ChangedPropertyNotification.AddedItems[propertyName].Add(item);
                }
            }
            else if (e is { Action: NotifyCollectionChangedAction.Remove, OldItems: not null })
            {
                // track the changes
                if (!target.ChangedPropertyNotification.RemovedItems.ContainsKey(propertyName))
                    target.ChangedPropertyNotification.RemovedItems.Add(propertyName, new List<object>());
                foreach (var item in e.OldItems)
                {
                    target.ChangedPropertyNotification.RemovedItems[propertyName].Add(item);
                }
            }
        };
    }

    class NotifyPropertyChangedInterceptor : IInterceptor
    {
        public bool InterceptionEnabled { get; set; } = true;
        
        public void Intercept(IInvocation invocation)
        {
            if (!InterceptionEnabled)
            {
                invocation.Proceed();
                return;
            }
            
            if (invocation.Method.Name == "Equals" && invocation.Arguments.Length == 1)
            {
                var otherProxy = invocation.Arguments[0];
                if (otherProxy != null && ProxyUtil.IsProxy(otherProxy))
                {
                    var target = invocation.InvocationTarget;
                    var otherTarget = ProxyUtil.GetUnproxiedInstance(otherProxy);
                    invocation.ReturnValue = target.Equals(otherTarget);
                    return;
                }
            }
            
            if (invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var propertyName = invocation.Method.Name[4..];

                var property = invocation.InvocationTarget.GetType().GetProperty(propertyName)!;
                var currentValue = property.GetValue(invocation.InvocationTarget)!;

                invocation.Proceed();

                var newValue = property.GetValue(invocation.InvocationTarget);

                // if value changed
                if (!Equals(currentValue, newValue) && invocation.Proxy is IHasChangedPropertyNotificationTrait proxy)
                {
                    // if assigned a new collection, binds the collection event again
                    if (newValue != null && newValue.GetType().IsObservableCollection())
                    {
                        WithObservableCollectionItemsProxy((IList)newValue);
                        BindOnCollectionChanged(propertyName, proxy, (INotifyCollectionChanged)newValue);
                        
                        // TODO add all previous values to removed items array
                    }
                    else
                    {
                        // if assigned a new value to a member which is observable, make it a proxy
                        if (typeof(IHasChangedPropertyNotificationTrait).IsAssignableFrom(property.PropertyType))
                            property.SetValue(invocation.InvocationTarget, WithObservableProxy(property.PropertyType, property.GetValue(proxy)!));

                        OnPropertyChanged(propertyName, proxy);
                    }
                }
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
    
    /// <summary>
    /// Proxy hook configured to intercept all methods, Object.Equals as well.
    /// </summary>
    class GeneralProxyGenerationHook : IProxyGenerationHook
    {
        // This method is called when all methods have been inspected.
        public void MethodsInspected()
        {
        }

        // This method is called when a non-proxyable member (like a non-virtual method) is encountered.
        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
        }

        // This method determines if a given method should be intercepted.
        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            // General rule: Intercept all methods.
            return true;
        }

        // Override Equals to ensure proper caching behavior
        public override bool Equals(object obj)
        {
            // General implementation: All instances of this hook are considered equal
            if (obj is GeneralProxyGenerationHook)
            {
                return true;
            }
            return false;
        }

        // Override GetHashCode to ensure proper caching behavior
        public override int GetHashCode()
        {
            // Return a constant hash code. Since all instances are considered equal, the hash code should be the same.
            return typeof(GeneralProxyGenerationHook).GetHashCode();
        }
    }
}