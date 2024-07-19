using System.Collections.Generic;
using Newtonsoft.Json;

namespace Looplex.DotNet.Core.Domain
{
    public partial class PaginatedCollection
    {
        /// <summary>
        /// The current page number
        /// </summary>
        [JsonProperty("page")]
        public long Page { get; set; }

        /// <summary>
        /// The number of items per page
        /// </summary>
        [JsonProperty("perPage")]
        public long PerPage { get; set; }

        /// <summary>
        /// The collection of records
        /// </summary>
        [JsonProperty("records")]
        public List<object> Records { get; set; } = new List<object>();

        /// <summary>
        /// The total number of items
        /// </summary>
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }
    }
}
