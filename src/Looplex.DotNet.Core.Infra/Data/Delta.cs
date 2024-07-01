using Looplex.DotNet.Core.Domain;
using System.Reflection;

namespace Looplex.DotNet.Core.Infra.Data
{
    public class Delta<T> : IDelta<T> where T : class
    {
        private readonly Dictionary<string, object> propertyChanges = new ();

        public bool TrySetProperty(string propertyName, object value)
        {
            var propertyInfo = GetPropertyInfo(propertyName);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyChanges[propertyName] = value;
                return true;
            }
            return false;
        }

        public bool TryGetProperty(string propertyName, out object? value)
        {
            value = null;
            if (propertyChanges.TryGetValue(propertyName, out var storedValue))
            {
                value = storedValue;
                return true;
            }
            return false;
        }

        public void ApplyChanges(T target)
        {
            foreach (var change in propertyChanges)
            {
                SetProperty(target, change.Key, change.Value);
            }
        }

        public IEnumerable<string> GetChangedProperties()
        {
            return propertyChanges.Keys;
        }

        private static PropertyInfo GetPropertyInfo(string propertyName)
        {
            return typeof(T).GetProperty(propertyName)!;
        }

        private static void SetProperty(T target, string propertyName, object value)
        {
            var propertyInfo = GetPropertyInfo(propertyName);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(target, value);
            }
        }
    }
}
