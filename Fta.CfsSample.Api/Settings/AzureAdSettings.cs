namespace Fta.CfsSample.Api.Settings
{
    public class AzureAdSettings
    {
        public const string AzureAdSectionKey = "AzureAD";
        
        public string Instance { get; set; } = default!;
        public string TenantId { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
    }
}
