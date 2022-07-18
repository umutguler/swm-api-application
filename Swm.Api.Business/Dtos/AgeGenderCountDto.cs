namespace Swm.Api.Business.Dtos
{
    public class AgePerGenderDto
    {
        /// <summary>
        /// Dictionary to represent the data
        /// Key: Age
        /// Value: { Gender as Key, Count as Value }
        /// </summary>
        public SortedDictionary<int, Dictionary<string, int>> AgePerGender { get; set; }

        public AgePerGenderDto()
        {
            AgePerGender = new SortedDictionary<int, Dictionary<string, int>>();
        }

        /// <summary>
        /// Custom pretty-printer for Dictionary
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            string _string = "";

            foreach (var age in AgePerGender.Keys)
            {
                _string += string.Format("{0}: ", age);
                foreach (var gender in AgePerGender[age].Keys)
                {
                    _string += string.Format("{0} : {1}", gender.ToUpper(), AgePerGender[age][gender]);
                    if (gender != AgePerGender[age].Keys.Last())
                    {
                        _string += ", ";
                    }
                }
                _string += "\n";
            }

            return _string ?? string.Empty;
        }
    }
}
