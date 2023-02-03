using Microsoft.Extensions.DependencyInjection;
using System;
using Azure.CfS.Library.Interfaces;
using Azure.CfS.Library.Services;
using Azure.CfS.Library.Logging;

namespace Azure.CfS.Library
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCfsLibrary(this IServiceCollection services, string cfsApiBaseUrl, string cfsApiPrimaryKey)
        {
            if (string.IsNullOrEmpty(cfsApiBaseUrl) || string.IsNullOrEmpty(cfsApiPrimaryKey))
            {
                throw new ArgumentException($"The parameters {nameof(cfsApiBaseUrl)} and {nameof(cfsApiPrimaryKey)} cannot be null or empty.");
            }
            
            services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            services
                .AddHttpClient<ICfsClient, CfsClient>(client =>
                {
                    client.BaseAddress = new Uri(cfsApiBaseUrl!);
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", cfsApiPrimaryKey);
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {"token-from-api-management-client-playground"}");
                });

            return services;
        }
    }
}
