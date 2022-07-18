namespace Swm.Api.Business
{
#nullable disable // added these nullable bits as the newer versions of C# complain bout not setting models
    public interface IAppConfig
    {
        public string Environment { get; set; }

        public bool IsDevelopment { get; }
        public bool IsProduction { get; }
    }
#nullable enable
}
