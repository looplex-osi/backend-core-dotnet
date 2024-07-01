using MassTransit.Serialization;
using System.Text.RegularExpressions;

namespace Looplex.DotNet.Core.Application.Services
{
    public static class CollectionWebLinkingBuilder
    {
        const string template = "/documents?page={{page}}&per_page={{per_page}}${extra_querystring}";
        public static Dictionary<string, object> BuildCollectionMetadata(Dictionary<string, object> datasource)
        {
            string template = datasource["template"].ToString()!;
            var page = Convert.ToInt32(datasource["page"]);
            var perPage = Convert.ToInt32(datasource["per_page"]);
            var pageCount = Convert.ToInt32(datasource["page_count"]);
            var totalCount = Convert.ToInt32(datasource["total_count"]);

            var self = Micromustache(template, new Dictionary<string, object> { { "page", page }, { "per_page", perPage } });
            var first = Micromustache(template, new Dictionary<string, object> { { "page", 1 }, { "per_page", perPage } });
            var last = Micromustache(template, new Dictionary<string, object> { { "page", pageCount }, { "per_page", perPage } });
            var prev = page > 1 ? Micromustache(template, new Dictionary<string, object> { { "page", page - 1 }, { "per_page", perPage } }) : null;
            var next = page < pageCount ? Micromustache(template, new Dictionary<string, object> { { "page", page + 1 }, { "per_page", perPage } }) : null;

            return new Dictionary<string, object>
            {
                { "page", page },
                { "per_page", perPage },
                { "page_count", pageCount },
                { "total_count", totalCount },
                { "links", new Dictionary<string, string>
                    {
                        { "self", self },
                        { "first", first },
                        { "last", last },
                        { "next", next },
                        { "prev", prev }
                    }
                }
            };
        }

        private static string Micromustache(string template, Dictionary<string, object> datasource)
        {
            var re = new Regex(@"\{\{([^}]+)\}\}");
            return re.Replace(template, match =>
            {
                var key = match.Groups[1].Value;
                datasource.TryGetValue(key, out string? value);
                return value ?? string.Empty;
            });
        }
    }
}
