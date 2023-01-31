using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fta.CfsSample.Api.Models
{
    public class GetEnrollmentProjectionsResponse
    {
        [JsonPropertyName("value")]
        public List<EnrollmentProjection> EnrollmentProjections { get; set; } = new List<EnrollmentProjection>();
    }

    public class EnrollmentProjection
    {
        [JsonPropertyName("actualEmissions")]
        public double? ActualEmissions { get; set; }

        [JsonPropertyName("actualUsage")]
        public double? ActualUsage { get; set; }

        [JsonPropertyName("dateKey")]
        public int? DateKey { get; set; }

        [JsonPropertyName("enrollmentId")]
        public string? EnrollmentId { get; set; }

        [JsonPropertyName("projectedEmissions")]
        public double? ProjectedEmissions { get; set; }

        [JsonPropertyName("projectedUsage")]
        public double? ProjectedUsage { get; set; }
    }
}
