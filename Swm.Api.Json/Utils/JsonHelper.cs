using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swm.Api.Json.Models;

namespace Swm.Api.Json.Utils
{
    /// <summary>
    /// Helper class to deserialize json objects
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Retrieves JSON data from Swm API
        /// Initially I was going to make it generic
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="logger"></param>
        /// <param name="route"></param>
        /// <returns>Task<IEnumerable<UserModel>></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<IEnumerable<UserModel>> GetJsonData(HttpClient? client, ILogger logger, string route = "")
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            SwmRootModel? model = null;

            try
            {
                Uri requestUri = new(route, UriKind.Relative);
                var response = await client.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                var data = (await response.Content.ReadAsStringAsync()).ToLower();
                model = JsonConvert.DeserializeObject<SwmRootModel>(data);
            }
            catch (HttpRequestException e)
            {
                logger.LogError("Please provide a valid URL for HTTP request");
                logger.LogError("\tsrc:\t{src}\n\tmsg:\t{msg}", e.Source, e.Message);
                logger.LogError("{e}", e.ToString());
            }
            catch (JsonException e)
            {
                logger.LogError("Please check JSON converter as destination API may have changed JSON formatting.");
                logger.LogError("\tsrc:\t{src}\n\tmsg:\t{msg}", e.Source, e.Message);
                logger.LogError("{e}", e.ToString());
            }
            catch (Exception e)
            {
                logger.LogError("\tsrc:\t{src}\n\tmsg:\t{msg}", e.Source, e.Message);
                logger.LogError("{e}", e.ToString());
            }

            return (model is not null) ? model.UserList : Enumerable.Empty<UserModel>();
        }
    }

}