using Newtonsoft.Json;

namespace Looplex.DotNet.Core.Domain
{
    public partial class PaginatedCollection
    {
    }

    public static class Serialize
    {
        public static string ToJson(this object self, JsonSerializerSettings settings) => JsonConvert.SerializeObject(self, settings);
    }
}
