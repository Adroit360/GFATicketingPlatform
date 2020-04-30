

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GFATicketing.Helpers
{
    public class Misc
    {
        public static JsonSerializerSettings getDefaultResolverJsonSettings()
        {
            return new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver()
            };
        }
    }
}
