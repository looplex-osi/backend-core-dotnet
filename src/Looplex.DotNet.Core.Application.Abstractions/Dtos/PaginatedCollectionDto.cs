using System.Collections.Generic;

namespace Looplex.DotNet.Core.Application.Abstractions.Dtos
{
    public class PaginatedCollectionDto
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalCount { get; set; }
    }

    public class PaginatedCollectionDto<T> : PaginatedCollectionDto
    {
        public IEnumerable<T> Records { get; set; } = [];
    }
}
