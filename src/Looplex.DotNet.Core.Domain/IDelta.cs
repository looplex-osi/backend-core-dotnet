using System.Collections.Generic;

namespace Looplex.DotNet.Core.Domain
{
    public interface IDelta<T> where T : class
    {
        bool TrySetProperty(string propertyName, object value);

        bool TryGetProperty(string propertyName, out object? value);

        void ApplyChanges(T target);

        IEnumerable<string> GetChangedProperties();
    }
}
