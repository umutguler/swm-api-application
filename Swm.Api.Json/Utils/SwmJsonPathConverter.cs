using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swm.Api.Json.Models;

namespace Swm.Api.Json.Utils
{
    /// <summary>
    /// Custom JsonConverter for Deserialization
    /// </summary>
    public class SwmJsonPathConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(SwmRootModel) == objectType;
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (!CanConvert(objectType)) return null;

            JArray jArray = JArray.Load(reader);

            SwmRootModel root = new()
            {
                UserList = jArray.ToObject<IEnumerable<UserModel>>()
            };

            return root;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}