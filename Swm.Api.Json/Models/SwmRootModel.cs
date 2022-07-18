using Newtonsoft.Json;
using Swm.Api.Json.Utils;

namespace Swm.Api.Json.Models
{
#nullable disable
    /// <summary>
    /// Some JSON serializer stuff
    /// </summary>
    [JsonConverter(typeof(SwmJsonPathConverter))]
    public class SwmRootModel
    {
        [JsonRequired]
        public IEnumerable<UserModel> UserList { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Include)]
        public string ErrorMessage { get; set; }
    }
#nullable enable
}