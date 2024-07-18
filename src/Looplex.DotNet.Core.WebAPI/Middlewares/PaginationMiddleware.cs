using System.Dynamic;
using Looplex.DotNet.Core.Application.Abstractions.DTOs;
using Looplex.DotNet.Core.Common.Utils;
using Looplex.DotNet.Core.Middlewares;
using Microsoft.AspNetCore.Http;

namespace Looplex.DotNet.Core.WebAPI.Middlewares
{
    public static partial class CoreMiddlewares
    {
        private const string ERR_TEMPLATE = "Param {0} must be specified for paginated resources";
        private const string ERR_TYPE_TEMPLATE = "Param {0} is not valid";
        private const int MIN_VALUE = 1;

        public readonly static MiddlewareDelegate PaginationMiddleware = new(async (context, next) =>
        {
            HttpContext httpContext = context.State.HttpContext;

            int page = GetQueryParam(httpContext, "page");
            int perPage = GetQueryParam(httpContext, "per_page");

            context.State.Pagination = new ExpandoObject();
            context.State.Pagination.Page = page;
            context.State.Pagination.PerPage = perPage;

            await next();

            var uri = GetUri(httpContext.Request);

            var totalCount = (int)context.Result.GetType()
                .GetProperty(nameof(PaginatedCollectionDTO.TotalCount))!
                .GetValue(context.Result)!;
            var linkHeader = PaginationUtils.CreateLinkHeader(uri, page, perPage, totalCount);

            httpContext.Response.Headers.Append("Link", linkHeader);
        });

        private static int GetQueryParam(HttpContext httpContext, string param)
        {
            string value;
            if (httpContext.Request.Query.TryGetValue(param, out var pageValue))
            {
                value = pageValue.ToString();
            }
            else
            {
                throw new ArgumentNullException(string.Format(ERR_TEMPLATE, param));
            }

            if (!int.TryParse(value, out int intValue) || intValue < MIN_VALUE)
            {
                throw new ArgumentException(string.Format(ERR_TYPE_TEMPLATE, param));
            }

            return intValue;
        }
        
        private static Uri GetUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80),
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };
            return uriBuilder.Uri;
        }
    }
}
