namespace Azure.CfS.Library.Options
{
    public class CfsLibraryOptions
    {
        public string CfsApiPrimaryKey { get; set; } = default!;
        public string CfsApiScope { get; set; } = "c3163bf1-092f-436b-b260-7ade5973e5b9/.default";
        public string CfsApiVersion { get; set; } = "v1.0";
        public string AzureAdTenantId { get; set; } = default!;
        public string AzureAdClientId { get; set; } = default!;
        public string AzureAdClientSecret { get; set; } = default!;
    }
}
