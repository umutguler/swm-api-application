using Newtonsoft.Json;

namespace Swm.Api.Json.Models
{
#nullable disable
    public class UserModel : IUserModel
    {
        [JsonRequired]
        public int Id { get; set; }

        [JsonRequired]
        public string First { get; set; }

        [JsonRequired]
        public string Last { get; set; }

        [JsonRequired]
        public int Age { get; set; }

        [JsonRequired]
        public string Gender { get; set; }
    }
#nullable enable
}