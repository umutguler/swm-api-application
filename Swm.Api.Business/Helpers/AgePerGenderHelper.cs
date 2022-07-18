using Swm.Api.Business.Dtos;
using Swm.Api.Json.Models;

namespace Swm.Api.Business.Helpers
{
    /// <summary>
    /// Helper class for AgePerGender task
    /// </summary>
    public static class AgePerGenderHelper
    {
        /// <summary>
        /// Generates the Dictionary for the Age-Per-Gender
        /// </summary>
        /// <param name="users"></param>
        /// <returns>AgePerGenderDto</returns>
        public static AgePerGenderDto GenerateDictionary(List<UserModel> users)
        {
            var agePerGenderDto = new AgePerGenderDto();
            var agePerGender = agePerGenderDto.AgePerGender;

            foreach (var user in users)
            {
                user.Gender = user.Gender;

                var genderAndCount = new Dictionary<string, int>();

                if (!genderAndCount.ContainsKey(user.Gender))
                {
                    genderAndCount.Add(user.Gender, 1);
                }

                if (!agePerGender.ContainsKey(user.Age))
                {
                    agePerGender.Add(user.Age, genderAndCount);
                }
                else
                {
                    if (!agePerGender[user.Age].ContainsKey(user.Gender))
                    {
                        agePerGender[user.Age].Add(user.Gender, 1);
                    }
                    else
                    {
                        agePerGender[user.Age][user.Gender] += 1;
                    }
                }
            }

            agePerGenderDto.AgePerGender = agePerGender;
            return agePerGenderDto;
        }

    }
}
