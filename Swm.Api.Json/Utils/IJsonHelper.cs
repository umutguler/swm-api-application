using Swm.Api.Json.Models;

namespace Swm.Api.Json.Utils
{
    public interface IJsonHelper
    {
        IEnumerable<UserModel> GetUsers();
    }
}