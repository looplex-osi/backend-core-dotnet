using System.Collections.Specialized;
using System.Web;

namespace Looplex.DotNet.Core.Common.Utils
{
    public static class PaginationUtils
    {
        public static int GetOffset(int perPage, int page)
        {
            return (page - 1) * perPage;
        }

        public static string CreateLinkHeader(Uri uri, int page, int perPage, int totalCount)
        {
            var links = new List<string>();

            // Calculate total pages
            var totalPages = CalculatePages(totalCount, perPage);

            NameValueCollection queryParams = HttpUtility.ParseQueryString(uri.Query);

            // Create link for self page
            queryParams["page"] = "1";
            var selfUri = new UriBuilder(uri)
            {
                Query = queryParams.ToString(),
            };
            links.Add($"<{selfUri}>; rel=\"self\"");

            // Create link for first page
            var firstUri = new Uri(uri, $"?page=1&perPage={perPage}");
            links.Add($"<{firstUri}>; rel=\"first\"");

            // Create link for previous page
            if (page > 1)
            {
                var prevPage = page - 1;
                queryParams["page"] = $"{prevPage}";
                var prevUri = new UriBuilder(uri)
                {
                    Query = queryParams.ToString(),
                };
                links.Add($"<{prevUri}>; rel=\"prev\"");
            }

            // Create link for next page
            if (page < totalPages)
            {
                var nextPage = page + 1;
                queryParams["page"] = $"{nextPage}";
                var nextUri = new UriBuilder(uri)
                {
                    Query = queryParams.ToString(),
                };
                links.Add($"<{nextUri}>; rel=\"next\"");
            }

            // Create link for last page
            queryParams["page"] = $"{totalPages}";
            var lastUri = new UriBuilder(uri)
            {
                Query = queryParams.ToString(),
            };
            links.Add($"<{lastUri}>; rel=\"last\"");

            return string.Join(", ", links);
        }

        public static int CalculatePages(int totalCount, int perPage)
        {
            int totalPages = 1;
            if (totalCount > 0)
            {
                totalPages = (int)Math.Ceiling((double)totalCount / perPage);
            }
            return totalPages;
        }
    }
}
