using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Fta.CfsSample.Api.Interfaces;
using System.Threading;
using System.Web.Http;
using Fta.CfsSample.Api.Options;
using Fta.CfsSample.Api.Models;

namespace Fta.CfsSample.Api
{
    public class CfsFunction
    {
        private readonly ICfsService _cfsService;
        private readonly ILoggerAdapter<CfsFunction> _logger;

        public CfsFunction(ICfsService cfsService, ILoggerAdapter<CfsFunction> logger)
        {
            _cfsService = cfsService;
            _logger = logger;
        }
        
        [FunctionName("GetEmissions")]
        public async Task<ActionResult<GetEnrollmentEmissionsResponse>> GetEmissions(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cfs/instances/{instanceId:guid}/enrollments/{enrollmentId:alpha}/operations/emissions")] HttpRequest req,
            Guid instanceId,
            string enrollmentId,
            CancellationToken ct = default)
        {
            _logger.LogInformation("C# HTTP trigger GetEmissions processed a request.");

            try
            {
                var emissionsByEnrollment = await _cfsService.GetEmissionsByEnrollmentAsync(new CfsApiOptions(instanceId, enrollmentId, req.Query), ct);

                if (emissionsByEnrollment.Error is not null)
                {
                    return new BadRequestObjectResult(emissionsByEnrollment.Error.Message);
                }

                return new OkObjectResult(emissionsByEnrollment.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsFunction)} -> {nameof(GetEmissions)} method.");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetMetadata")]
        public async Task<ActionResult<string>> GetMetadata(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cfs/instances/{instanceId:guid}/enrollments/{enrollmentId:alpha}/operations/$metadata")] HttpRequest req,
            Guid instanceId,
            string enrollmentId,
            CancellationToken ct = default)
        {
            _logger.LogInformation("C# HTTP trigger GetMetadata processed a request.");

            try
            {
                var metadata = await _cfsService.GetMetadataAsync(new CfsApiOptions(instanceId, enrollmentId, req.Query), ct);

                if (metadata.Error is not null)
                {
                    return new BadRequestObjectResult(metadata.Error.Message);
                }

                return new OkObjectResult(metadata.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsFunction)} -> {nameof(GetMetadata)} method.");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetProjections")]
        public async Task<ActionResult<GetEnrollmentProjectionsResponse>> GetProjections(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cfs/instances/{instanceId:guid}/enrollments/{enrollmentId:alpha}/operations/projections")] HttpRequest req,
            Guid instanceId,
            string enrollmentId,
            CancellationToken ct = default)
        {
            _logger.LogInformation("C# HTTP trigger GetProjections processed a request.");

            try
            {
                var projectionsByEnrollment = await _cfsService.GetProjectionsByEnrollmentAsync(new CfsApiOptions(instanceId, enrollmentId, req.Query), ct);

                if (projectionsByEnrollment.Error is not null)
                {
                    return new BadRequestObjectResult(projectionsByEnrollment.Error.Message);
                }

                return new OkObjectResult(projectionsByEnrollment.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsFunction)} -> {nameof(GetProjections)} method.");

                return new InternalServerErrorResult();
            }
        }

        [FunctionName("GetUsage")]
        public async Task<ActionResult<GetEnrollmentUsagesResponse>> GetUsage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "cfs/instances/{instanceId:guid}/enrollments/{enrollmentId:alpha}/operations/usage")] HttpRequest req,
            Guid instanceId,
            string enrollmentId,
            CancellationToken ct = default)
        {
            _logger.LogInformation("C# HTTP trigger GetUsage processed a request.");

            try
            {
                var usageByEnrollment = await _cfsService.GetUsageByEnrollmentAsync(new CfsApiOptions(instanceId, enrollmentId, req.Query), ct);

                if (usageByEnrollment.Error is not null)
                {
                    return new BadRequestObjectResult(usageByEnrollment.Error.Message);
                }

                return new OkObjectResult(usageByEnrollment.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsFunction)} -> {nameof(GetUsage)} method.");

                return new InternalServerErrorResult();
            }
        }
    }
}
