using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fta.CfsSample.Api.Models
{
    public class GetEnrollmentEmissionsResponse
    {
        [JsonPropertyName("value")]
        public List<EnrollmentEmission> EnrollmentEmissions { get; set; } = new List<EnrollmentEmission>();
    }
    
    public class EnrollmentEmission
    {
        [JsonPropertyName("azureRegionName")]
        public string? AzureRegionName { get; set; }

        [JsonPropertyName("azureServiceName")]
        public string? AzureServiceName { get; set; }
        
        [JsonPropertyName("dateKey")]
        public int? DateKey { get; set; }
        
        [JsonPropertyName("enrollmentId")]
        public string? EnrollmentId { get; set; }
        
        [JsonPropertyName("orgName")]
        public string? OrgName { get; set; }
        
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
        
        [JsonPropertyName("scopeId")]
        public int? ScopeId { get; set; }
        
        [JsonPropertyName("subService")]
        public string? SubService { get; set; }
        
        [JsonPropertyName("subscriptionId")]
        public string? SubscriptionId { get; set; }
        
        [JsonPropertyName("subscriptionName")]
        public string? SubscriptionName { get; set; }
        
        [JsonPropertyName("totalEmissions")]
        public double? TotalEmissions { get; set; }
    }
}
