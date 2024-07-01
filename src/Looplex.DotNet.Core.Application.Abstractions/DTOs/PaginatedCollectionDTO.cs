using System.Collections.Generic;

namespace Looplex.DotNet.Core.Application.Abstractions.Pagination
{
    public class PaginatedCollectionDTO
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class PaginatedCollectionDTO<T> : PaginatedCollectionDTO
    {
        public IEnumerable<T> Records { get; set; } = [];
    }
}
