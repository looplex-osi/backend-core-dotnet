using System.Collections.Generic;
using Newtonsoft.Json;

namespace Looplex.DotNet.Core.Domain.Traits
{
    public interface IChangedPropertyNotificationTrait 
    {
        [JsonIgnore]
        public IList<string> ChangedProperties { get; }
        [JsonIgnore]
        public IDictionary<string, IList<object>> AddedItems { get; }
        [JsonIgnore]
        public IDictionary<string, IList<object>> RemovedItems { get; }
        
        #region Events

        public string PropertyChangedEventName => "PropertyChanged";

        public string CollectionChangedEventName => "CollectionChanged";
        
        #endregion
    }
}