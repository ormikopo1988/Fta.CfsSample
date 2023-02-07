using Azure.CfS.Library.Contracts;
using Azure.CfS.Library.Interfaces;
using Azure.CfS.Library.Models;
using Azure.CfS.Library.Options;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.CfS.Library.Services
{
    public class CfsAuthorizationClientDecorator : ICfsClient
    {
        private readonly ICfsClient _innerCfsClient;
        private readonly IOptions<AuthorizationClientOptions> _authorizationClientOptions;
        private readonly IConfidentialClientApplication _confidentialClientApplication;
        private readonly ILoggerAdapter<CfsAuthorizationClientDecorator> _logger;

        public CfsAuthorizationClientDecorator(ICfsClient innerCfsClient, 
            IOptions<AuthorizationClientOptions> authorizationClientOptions, 
            IConfidentialClientApplication confidentialClientApplication, 
            ILoggerAdapter<CfsAuthorizationClientDecorator> logger)
        {
            _innerCfsClient = innerCfsClient;
            _authorizationClientOptions = authorizationClientOptions;
            _confidentialClientApplication = confidentialClientApplication;
            _logger = logger;
        }

        public async Task<Result<GetEnrollmentEmissionsResponse>> GetEmissionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(cfsApiOptions.AccessToken))
            {
                cfsApiOptions.AccessToken = await GetClientCredentialsTokenAsync(ct).ConfigureAwait(false);
            }

            return await _innerCfsClient.GetEmissionsByEnrollmentAsync(cfsApiOptions, ct).ConfigureAwait(false);
        }

        public async Task<Result<string>> GetMetadataAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(cfsApiOptions.AccessToken))
            {
                cfsApiOptions.AccessToken = await GetClientCredentialsTokenAsync(ct).ConfigureAwait(false);
            }

            return await _innerCfsClient.GetMetadataAsync(cfsApiOptions, ct).ConfigureAwait(false);
        }

        public async Task<Result<GetEnrollmentProjectionsResponse>> GetProjectionsByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(cfsApiOptions.AccessToken))
            {
                cfsApiOptions.AccessToken = await GetClientCredentialsTokenAsync(ct).ConfigureAwait(false);
            }

            return await _innerCfsClient.GetProjectionsByEnrollmentAsync(cfsApiOptions, ct).ConfigureAwait(false);
        }

        public async Task<Result<GetEnrollmentUsagesResponse>> GetUsageByEnrollmentAsync(CfsApiOptions cfsApiOptions, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(cfsApiOptions.AccessToken))
            {
                cfsApiOptions.AccessToken = await GetClientCredentialsTokenAsync(ct).ConfigureAwait(false);
            }

            return await _innerCfsClient.GetUsageByEnrollmentAsync(cfsApiOptions, ct).ConfigureAwait(false);
        }

        private async Task<string> GetClientCredentialsTokenAsync(CancellationToken ct)
        {
            AuthenticationResult? result = null;

            try
            {
                result = await _confidentialClientApplication.AcquireTokenForClient(_authorizationClientOptions.Value.ResourceIds).ExecuteAsync(ct).ConfigureAwait(false);
            }
            catch (MsalUiRequiredException ex)
            {
                // The application doesn't have sufficient permissions.
                // - Did you declare enough app permissions during app creation?
                // - Did the tenant admin grant permissions to the application?
                _logger.LogError(ex, $"{nameof(MsalUiRequiredException)} in {nameof(CfsAuthorizationClientDecorator)} -> {nameof(GetClientCredentialsTokenAsync)} method.");
            }
            catch (MsalServiceException ex) when (ex.Message.Contains(Constants.InvalidScopeErrorCode))
            {
                // Invalid scope. The scope has to be in the form "https://resourceurl/.default"
                // Mitigation: Change the scope to be as expected.
                _logger.LogError(ex, $"{nameof(MsalServiceException)} in {nameof(CfsAuthorizationClientDecorator)} -> {nameof(GetClientCredentialsTokenAsync)} method.");
            }

            if (result is null || string.IsNullOrEmpty(result.AccessToken))
            {
                throw new InvalidOperationException("Could not fetch access token to call the MCfS API.");
            }

            return result.AccessToken;
        }
    }
}
