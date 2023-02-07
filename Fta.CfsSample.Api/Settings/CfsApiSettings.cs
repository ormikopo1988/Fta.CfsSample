namespace Fta.CfsSample.Api.Settings
{
    public class CfsApiSettings
    {
        public const string CfsApiSectionKey = "CfsApi";

        public string PrimaryKey { get; set; } = default!;
        public string BaseUrl { get; set; } = default!;
        public string Scope { get; set; } = default!;
    }
}
