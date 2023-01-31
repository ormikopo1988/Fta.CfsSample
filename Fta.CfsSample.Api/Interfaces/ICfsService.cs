using Fta.CfsSample.Api.Models;
using Fta.CfsSample.Api.Options;
using System.Threading;
using System.Threading.Tasks;

namespace Fta.CfsSample.Api.Interfaces
{
    public interface ICfsService
    {
        Task<Result<GetEnrollmentEmissionsResponse>> GetEmissionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct);
        Task<Result<string>> GetMetadataAsync(CfsApiOptions cfsApiOptions, CancellationToken ct);
        Task<Result<GetEnrollmentProjectionsResponse>> GetProjectionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct);
        Task<Result<GetEnrollmentUsagesResponse>> GetUsageByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct);
    }
}
