using System.Collections.Generic;

namespace Looplex.DotNet.Core.Domain.Traits
{
    public sealed class ChangedPropertyNotificationTrait : IChangedPropertyNotificationTrait 
    {
        public IList<string> ChangedProperties { get; } = new List<string>();
        public IDictionary<string, IList<object>> AddedItems { get; } = new Dictionary<string, IList<object>>();
        public IDictionary<string, IList<object>> RemovedItems { get; } =  new Dictionary<string, IList<object>>();

        #region Events

        public string PropertyChangedEventName => "PropertyChanged";

        public string CollectionChangedEventName => "CollectionChanged";

        #endregion
    }
}