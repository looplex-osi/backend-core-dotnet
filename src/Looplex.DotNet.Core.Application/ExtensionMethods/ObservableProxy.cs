using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;
using Castle.DynamicProxy;
using Looplex.DotNet.Core.Domain.Traits;
using Looplex.OpenForExtension.Abstractions.Traits;

namespace Looplex.DotNet.Core.Application.ExtensionMethods;

public static class ObservableProxy
{
    private static readonly ProxyGenerator ProxyGenerator = new();
    
    public static T WithObservableProxy<T>(this T instance) where T : class, IHasChangedPropertyNotificationTrait, new()
    {
        var proxy = ProxyGenerator.CreateClassProxyWithTarget<T>(instance,new NotifyPropertyChangedInterceptor());
        
        var setters = typeof(T)
            .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(m => m.IsSpecialName && m.Name.StartsWith("set_"));
        
        foreach (var setter in setters)
        {
            var propertyName = setter.Name[4..];
            var property = typeof(T).GetProperty(propertyName)!;
            
            if (property.GetValue(proxy) is INotifyCollectionChanged collection)
            {
                BindOnCollectionChanged(property.Name, proxy, collection);
            }
        }

        return proxy;
    }

    private static void OnPropertyChanged(string propertyName, IHasChangedPropertyNotificationTrait target)
    {
        if (target is IHasEventHandlerTrait eventHandlerTrait)
            eventHandlerTrait.EventHandling.Invoke(
                target.ChangedPropertyNotification.PropertyChangedEventName, 
                target,
                new PropertyChangedEventArgs(propertyName));
        
        if (!target.ChangedPropertyNotification.ChangedProperties.Contains(propertyName))
            target.ChangedPropertyNotification.ChangedProperties.Add(propertyName);
    }

    private static void BindOnCollectionChanged(string propertyName, IHasChangedPropertyNotificationTrait target,
        INotifyCollectionChanged collection)
    {
        collection.CollectionChanged += (_, e) =>
        {
            if (target is IHasEventHandlerTrait eventHandlerTrait)
                eventHandlerTrait.EventHandling.Invoke(
                    target.ChangedPropertyNotification.CollectionChangedEventName,
                    target,
                    e);
            
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (!target.ChangedPropertyNotification.AddedItems.ContainsKey(propertyName))
                    target.ChangedPropertyNotification.AddedItems.Add(propertyName, new List<object>());
                foreach (var item in e.NewItems!)
                {
                    target.ChangedPropertyNotification.AddedItems[propertyName].Add(item);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                if (!target.ChangedPropertyNotification.RemovedItems.ContainsKey(propertyName))
                    target.ChangedPropertyNotification.RemovedItems.Add(propertyName, new List<object>());
                foreach (var item in e.OldItems!)
                {
                    target.ChangedPropertyNotification.RemovedItems[propertyName].Add(item);
                }
            }
            else throw new InvalidOperationException($"Cannot perform {e.Action} action on {propertyName}");
        };
    }

    class NotifyPropertyChangedInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.IsSpecialName && invocation.Method.Name.StartsWith("set_"))
            {
                var propertyName = invocation.Method.Name[4..];

                var property = invocation.InvocationTarget.GetType().GetProperty(propertyName)!;
                var currentValue = property.GetValue(invocation.InvocationTarget)!;

                invocation.Proceed();

                var newValue = property.GetValue(invocation.InvocationTarget);

                if (!Equals(currentValue, newValue) && invocation.Proxy is IHasChangedPropertyNotificationTrait proxy)
                {
                    if (newValue is INotifyCollectionChanged newCollection)
                    {
                        BindOnCollectionChanged(propertyName, proxy, newCollection);
                    }
                    else
                    {
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
}