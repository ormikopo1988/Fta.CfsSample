using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fta.CfsSample.Api.Models
{
    public class GetEnrollmentUsagesResponse
    {
        [JsonPropertyName("value")]
        public List<EnrollmentUsage> EnrollmentUsage { get; set; } = new List<EnrollmentUsage>();
    }

    public class EnrollmentUsage
    {
        [JsonPropertyName("azureRegionName")]
        public string? AzureRegionName { get; set; }

        [JsonPropertyName("dateKey")]
        public int? DateKey { get; set; }

        [JsonPropertyName("enrollmentId")]
        public string? EnrollmentId { get; set; }

        [JsonPropertyName("orgName")]
        public string? OrgName { get; set; }

        [JsonPropertyName("subService")]
        public string? SubService { get; set; }

        [JsonPropertyName("subscriptionId")]
        public string? SubscriptionId { get; set; }

        [JsonPropertyName("subscriptionName")]
        public string? SubscriptionName { get; set; }

        [JsonPropertyName("usage")]
        public double? Usage { get; set; }
    }
}
