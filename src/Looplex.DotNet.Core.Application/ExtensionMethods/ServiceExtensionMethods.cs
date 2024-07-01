using Looplex.OpenForExtension.Context;
using System.Dynamic;

namespace Looplex.DotNet.Core.Application.ExtensionMethods
{
    public static class ServiceExtensionMethods
    {
        public static T GetRequiredValue<T>(this IDefaultContext context, string path)
        {
            var value = GetValue<T>(context, path);

            if (value == null)
            {
                throw new ArgumentNullException(nameof(path), $"The path '{path}' does not exist or has a null value.");
            }

            return value;
        }

        public static T? GetValue<T>(this IDefaultContext context, string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            var currentObject = (object)context.State;
            var properties = path.Split('.');

            foreach (var property in properties)
            {
                if (currentObject is ExpandoObject expando)
                {
                    if ((expando as IDictionary<string, object>).TryGetValue(property, out var nextObject))
                    {
                        currentObject = nextObject;
                    }
                    else
                    {
                        return default; 
                    }
                }
                else
                {
                    return default; 
                }
            }

            return (T?)currentObject;
        }
    }
}
