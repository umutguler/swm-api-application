namespace Swm.Api.Business
{
#nullable disable
    /// <summary>
    /// AppConfig is for the appsettings.json to determine the environment.
    /// </summary>
    public class AppConfig : IAppConfig
    {
        public Uri ApiUri { get; set; }
        public string Environment { get; set; }
        public bool IsDevelopment { get { return (Environment == "Development"); } }
        public bool IsProduction { get { return (Environment == "Production"); } }
    }
#nullable enable
}
