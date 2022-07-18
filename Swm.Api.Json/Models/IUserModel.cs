namespace Swm.Api.Json.Models
{
#nullable disable
    public interface IUserModel
    {
        public int Id { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
#nullable enable
}