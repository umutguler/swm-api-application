using Swm.Api.Business.Dtos;
using Swm.Api.Json.Models;

namespace Swm.Api.Business.Services
{
    /// <summary>
    /// Interface for SwmApiService to represent the various functions and enable DI
    /// </summary>
    public interface ISwmApiService
    {
        Task<UserModel?> GetUserByIdFirstOrDefault(int id);
        Task<string> GetFullNameById(int id);
        Task<IEnumerable<UserModel>> GetUsersByAge(int age);
        Task<string> GetFirstNamesByAgeCsv(int age);
        Task<AgePerGenderDto> GetGenderCountPerAge();
        Task<List<UserModel>> RetrieveUsers();
    }
}
