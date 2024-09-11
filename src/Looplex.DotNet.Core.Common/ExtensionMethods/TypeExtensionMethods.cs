using System.Collections;
using System.Collections.ObjectModel;

namespace Looplex.DotNet.Core.Common.ExtensionMethods;

public static class TypeExtensionMethods
{
    public static bool IsNonStringEnumerable(this Type? type)
    {
        if (type == null) return false;

        return type != typeof (string) &&
               ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof (IEnumerable<>)) ||
                type.GetInterfaces()
                    .Any(
                        interfaceType =>
                            (interfaceType.IsGenericType &&
                             interfaceType.GetGenericTypeDefinition() == typeof (IEnumerable<>)) ||
                            (interfaceType == typeof (IEnumerable))));
    }

    public static bool IsObservableCollection(this Type type)
    {
        return type != typeof (string) &&
               ((type.IsGenericType && type.GetGenericTypeDefinition() == typeof (ObservableCollection<>)) ||
                type.GetInterfaces()
                    .Any(
                        interfaceType =>
                            (interfaceType.IsGenericType &&
                             interfaceType.GetGenericTypeDefinition() == typeof (ObservableCollection<>))));
    }
}