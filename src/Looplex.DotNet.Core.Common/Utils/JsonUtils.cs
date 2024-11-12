using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json.Linq;

namespace Looplex.DotNet.Core.Common.Utils
{
    public static class JsonUtils
    {
        public const string JsonContentTypeWithCharset = "application/json; charset=utf-8";

        public static Task WriteAsJsonAsync(this HttpResponse httpResponse, string serializedValue, HttpStatusCode httpStatusCode)
        {
            httpResponse.ContentType = JsonContentTypeWithCharset;
            httpResponse.StatusCode = (int)httpStatusCode;
            return httpResponse.WriteAsync(serializedValue);
        }
        
        public static JsonSerializerOptions HttpBodyConverter()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        
        public static void Traverse(JToken token, Action<JToken> visitor)
        {
            visitor(token);

            foreach (var child in token.Children())
            {
                Traverse(child, visitor);
            }
        }
    }
}
