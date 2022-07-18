using Microsoft.Extensions.Logging;
using Swm.Api.Business.Dtos;
using Swm.Api.Business.Helpers;
using Swm.Api.Json.Models;
using Swm.Api.Json.Utils;

namespace Swm.Api.Business.Services
{
    /// <summary>
    /// Main class to represent the various functions
    /// This can be broken down/segregated further but for this case it's left as is. I've somewhat broken it down though.
    /// 
    /// Contains a HttpClientFactory and Logger
    /// </summary>
    public class SwmApiService : ISwmApiService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;

        /// <summary>
        /// ClientFactory and Logger DI
        /// </summary>
        /// <param name="clientFactory"></param>
        /// <param name="loggerFactory"></param>
        public SwmApiService(IHttpClientFactory clientFactory, ILoggerFactory loggerFactory)
        {
            _clientFactory = clientFactory;
            _logger = loggerFactory.CreateLogger("SwmServiceLogger");
        }

        /// <summary>
        /// Gets the user by ID
        /// First value it finds or defaults to null
        /// </summary>
        /// <param name="id"></param>
        /// <returns>UserModel?</returns>
        public async Task<UserModel?> GetUserByIdFirstOrDefault(int id)
        {
            List<UserModel> users = await RetrieveUsers();
            var user = users.Where(x => x.Id == id);

            if (user.Count() > 1)
            {
                _logger.LogWarning("There were multiple ids of the same value for ID:\t{id}", id);
                _logger.LogWarning("Picking first one available!");
            }
            return user.FirstOrDefault();
        }

        /// <summary>
        /// Handles the above function to get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>string</returns>
        public async Task<string> GetFullNameById(int id)
        {
            var user = await GetUserByIdFirstOrDefault(id);

            if (user is not null)
            {
                _logger.LogInformation("Successful attempt to get user full name for ID:\t{id}", id);

                return string.Format("{0} {1}", user.First, user.Last);
            }
            else
            {
                _logger.LogWarning("Failed attempt to get user full name for ID:\t{id}", id);

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the users by age
        /// </summary>
        /// <param name="age"></param>
        /// <returns>UserModel</returns>
        public async Task<IEnumerable<UserModel>> GetUsersByAge(int age)
        {
            List<UserModel> users = await RetrieveUsers();

            return users.Where(x => x.Age == age).ToList();
        }

        /// <summary>
        /// Gets the users by age and puts it into CSV format for the first name
        /// </summary>
        /// <param name="age"></param>
        /// <returns>string</returns>
        public async Task<string> GetFirstNamesByAgeCsv(int age)
        {
            List<UserModel> users = (await GetUsersByAge(age)).ToList();
            string csvFirstNames = string.Empty;

            foreach (var user in users)
            {
                csvFirstNames += user.First;
                if (user != users.Last())
                {
                    csvFirstNames += ",";
                }
            }

            if (csvFirstNames == string.Empty)
            {
                _logger.LogWarning("Failed attempt to locate a user/s full name for age of:\t{age}", age);
            }

            return csvFirstNames;
        }

        /// <summary>
        /// Gets the gender count per age
        /// </summary>
        /// <returns>AgePerGenderDto</returns>
        public async Task<AgePerGenderDto> GetGenderCountPerAge()
        {
            List<UserModel> users = await RetrieveUsers();
            _logger.LogInformation("Creating dictionary of age per gender.");
            AgePerGenderDto agePerGenderDto = AgePerGenderHelper.GenerateDictionary(users);
            _logger.LogInformation("Dictionary created successfully!\nResult:\n{result}", agePerGenderDto.ToString());

            return agePerGenderDto;
        }


        /// <summary>
        /// Retrieves list of users from the API
        /// </summary>
        /// <returns>List<UserModel></returns>
        public async Task<List<UserModel>> RetrieveUsers()
        {
            List<UserModel> users;
            using (var client = _clientFactory.CreateClient("swm"))
            {
                try
                {
                    users = (await JsonHelper.GetJsonData(client, _logger)).ToList();
                    _logger.LogInformation("Users retrieved successfully!");
                    _logger.LogInformation("Users count:\t{count}", users.Count);
                }
                catch (Exception e)
                {
                    _logger.LogError("Please ensure http client is set correctly");
                    _logger.LogError("\tsrc:\t{src}\n\tmsg:\t{msg}", e.Source, e.Message);
                    _logger.LogError("{e}", e.ToString());
                    throw;
                }
            }

            return users;
        }
    }
}