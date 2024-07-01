using System.Collections.Generic;

namespace Looplex.DotNet.Core.Domain
{
    public class PaginatedCollection<T>
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Records { get; set; } = new List<T>();
    }
}
