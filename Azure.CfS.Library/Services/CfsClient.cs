using Azure.CfS.Library.Contracts;
using Azure.CfS.Library.Interfaces;
using Azure.CfS.Library.Models;
using Azure.CfS.Library.Options;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.CfS.Library.Services
{
    public class CfsClient : ICfsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerAdapter<CfsClient> _logger;

        public CfsClient(HttpClient httpClient, ILoggerAdapter<CfsClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        
        public async Task<Result<GetEnrollmentEmissionsResponse>> GetEmissionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            GetEnrollmentEmissionsResponse? enrollmentEmissionsResponse = null;
            
            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(BuildUrl(cfsApiOptions, "emissions"), ct);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    enrollmentEmissionsResponse = await httpResponseMessage.Content.ReadFromJsonAsync<GetEnrollmentEmissionsResponse>(cancellationToken: ct);

                    return new Result<GetEnrollmentEmissionsResponse>
                    {
                        Data = enrollmentEmissionsResponse!
                    };
                }

                await LogErrorMessageAsync(httpResponseMessage, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsClient)} -> {nameof(GetEmissionsByEnrollmentAsync)} method.");
            }

            return new Result<GetEnrollmentEmissionsResponse>
            {
                Error = new Error
                {
                    Code = 999,
                    Message = "Unable to fetch emissions for enrollment."
                }
            };
        }

        public async Task<Result<string>> GetMetadataAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            string? metadataResponse = null;

            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(BuildUrl(cfsApiOptions, "$metadata"), ct);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    metadataResponse = await httpResponseMessage!.Content.ReadAsStringAsync(ct);

                    return new Result<string>
                    {
                        Data = metadataResponse!
                    };
                }

                await LogErrorMessageAsync(httpResponseMessage, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsClient)} -> {nameof(GetMetadataAsync)} method.");
            }

            return new Result<string>
            {
                Error = new Error
                {
                    Code = 999,
                    Message = "Unable to fetch metadata."
                }
            };
        }

        public async Task<Result<GetEnrollmentProjectionsResponse>> GetProjectionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            GetEnrollmentProjectionsResponse? enrollmentProjectionsResponse = null;

            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(BuildUrl(cfsApiOptions, "projections"), ct);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var tmp = await httpResponseMessage.Content.ReadAsStringAsync(ct);
                    
                    enrollmentProjectionsResponse = await httpResponseMessage.Content.ReadFromJsonAsync<GetEnrollmentProjectionsResponse>(cancellationToken: ct);

                    return new Result<GetEnrollmentProjectionsResponse>
                    {
                        Data = enrollmentProjectionsResponse!
                    };
                }

                await LogErrorMessageAsync(httpResponseMessage, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsClient)} -> {nameof(GetProjectionsByEnrollmentAsync)} method.");
            }

            return new Result<GetEnrollmentProjectionsResponse>
            {
                Error = new Error
                {
                    Code = 999,
                    Message = "Unable to fetch projections for enrollment."
                }
            };
        }

        public async Task<Result<GetEnrollmentUsagesResponse>> GetUsageByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            GetEnrollmentUsagesResponse? enrollmentUsagesResponse = null;

            try
            {
                var httpResponseMessage = await _httpClient.GetAsync(BuildUrl(cfsApiOptions, "usage"), ct);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    enrollmentUsagesResponse = await httpResponseMessage.Content.ReadFromJsonAsync<GetEnrollmentUsagesResponse>(cancellationToken: ct);

                    return new Result<GetEnrollmentUsagesResponse>
                    {
                        Data = enrollmentUsagesResponse!
                    };
                }

                await LogErrorMessageAsync(httpResponseMessage, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Exception in {nameof(CfsClient)} -> {nameof(GetUsageByEnrollmentAsync)} method.");
            }

            return new Result<GetEnrollmentUsagesResponse>
            {
                Error = new Error
                {
                    Code = 999,
                    Message = "Unable to fetch usage for enrollment."
                }
            };
        }

        private static string BuildUrl(CfsApiOptions cfsApiOptions, string operation)
        {
            var result = new StringBuilder($"instances/{cfsApiOptions.InstanceId}/enrollments/{cfsApiOptions.EnrollmentId}/{operation}");
            
            if (cfsApiOptions.QueryParams is not null)
            {
                result.Append('?');

                if (cfsApiOptions.QueryParams.Skip != default)
                {
                    result.Append($"$skip={cfsApiOptions.QueryParams.Skip}");
                    result.Append('&');
                }
                if (cfsApiOptions.QueryParams.Count != default)
                {
                    result.Append($"$count={cfsApiOptions.QueryParams.Count}");
                    result.Append('&');
                }
                if (!string.IsNullOrWhiteSpace(cfsApiOptions.QueryParams.Select))
                {
                    result.Append($"$select={cfsApiOptions.QueryParams.Select}");
                    result.Append('&');
                }
                if (!string.IsNullOrWhiteSpace(cfsApiOptions.QueryParams.Filter))
                {
                    result.Append($"$filter={cfsApiOptions.QueryParams.Filter}");
                    result.Append('&');
                }
                if (!string.IsNullOrWhiteSpace(cfsApiOptions.QueryParams.Apply))
                {
                    result.Append($"$apply={cfsApiOptions.QueryParams.Apply}");
                    result.Append('&');
                }
                if (!string.IsNullOrWhiteSpace(cfsApiOptions.QueryParams.Expand))
                {
                    result.Append($"$expand={cfsApiOptions.QueryParams.Expand}");
                    result.Append('&');
                }
                if (!string.IsNullOrWhiteSpace(cfsApiOptions.QueryParams.OrderBy))
                {
                    result.Append($"$orderby={cfsApiOptions.QueryParams.OrderBy}");
                    result.Append('&');
                }
                if (cfsApiOptions.QueryParams.Top != default)
                {
                    result.Append($"$top={cfsApiOptions.QueryParams.Top}");
                }
            }

            return result.ToString();
        }

        private async Task LogErrorMessageAsync(HttpResponseMessage httpResponseMessage, CancellationToken ct)
        {
            var errorMessage = await httpResponseMessage.Content?.ReadAsStringAsync(ct)!;

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                _logger.LogError(errorMessage);
            }
        }
    }
}
