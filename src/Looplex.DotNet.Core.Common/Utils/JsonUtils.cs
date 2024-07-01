using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Looplex.DotNet.Core.Common.Utils
{
    public static class JsonUtils
    {
        public const string JsonContentTypeWithCharset = "application/json; charset=utf-8";
        
        public static Task WriteAsJsonAsync(this HttpResponse httpResponse, object value, HttpStatusCode httpStatusCode)
        {
            httpResponse.ContentType = JsonContentTypeWithCharset;
            httpResponse.StatusCode = (int)httpStatusCode;
            return httpResponse.WriteAsync(JsonSerializer.Serialize(value));
        }

        public static JsonSerializerOptions HttpBodyConverter()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
    }
}
