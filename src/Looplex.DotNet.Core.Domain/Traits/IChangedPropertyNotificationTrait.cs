using System.Collections.Generic;
using Newtonsoft.Json;

namespace Looplex.DotNet.Core.Domain.Traits
{
    public interface IChangedPropertyNotificationTrait 
    {
        [JsonIgnore] 
        IList<string> ChangedProperties { get; }
        [JsonIgnore]
        IDictionary<string, IList<object>> AddedItems { get; }
        [JsonIgnore]
        IDictionary<string, IList<object>> RemovedItems { get; }
        
        #region Events

        string PropertyChangedEventName { get; }

        string CollectionChangedEventName { get; }
        
        #endregion
    }
}