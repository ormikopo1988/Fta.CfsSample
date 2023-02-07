﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Azure.CfS.Library.Interfaces;
using Azure.CfS.Library.Services;
using Azure.CfS.Library.Logging;
using Azure.CfS.Library.Options;
using Microsoft.Identity.Client;
using System.Globalization;

namespace Azure.CfS.Library
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCfsLibrary(this IServiceCollection services, CfsLibraryOptions cfsLibraryOptions)
        {
            if (cfsLibraryOptions is null)
            {
                throw new ArgumentNullException(nameof(cfsLibraryOptions));
            }
            
            if (string.IsNullOrEmpty(cfsLibraryOptions.CfsApiPrimaryKey))
            {
                throw new ArgumentException($"The parameter {nameof(cfsLibraryOptions.CfsApiPrimaryKey)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(cfsLibraryOptions.AzureAdClientId))
            {
                throw new ArgumentException($"The parameter {nameof(cfsLibraryOptions.AzureAdClientId)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(cfsLibraryOptions.AzureAdClientSecret))
            {
                throw new ArgumentException($"The parameter {nameof(cfsLibraryOptions.AzureAdClientSecret)} cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(cfsLibraryOptions.AzureAdTenantId))
            {
                throw new ArgumentException($"The parameter {nameof(cfsLibraryOptions.AzureAdTenantId)} cannot be null or empty.");
            }

            services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

            services.Configure<AuthorizationClientOptions>(aco =>
            {
                aco.ResourceIds = new string[] { cfsLibraryOptions.CfsApiScope };
            });

            services.AddSingleton(sp => {
                var authority = string.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}", cfsLibraryOptions.AzureAdTenantId);
                return ConfidentialClientApplicationBuilder.Create(cfsLibraryOptions.AzureAdClientId)
                    .WithClientSecret(cfsLibraryOptions.AzureAdClientSecret)
                    .WithAuthority(new Uri(authority))
                    .Build();
            });

            services
                .AddHttpClient<ICfsClient, CfsClient>(client =>
                {
                    client.BaseAddress = new Uri($"https://api.mcfs.microsoft.com/api/{cfsLibraryOptions.CfsApiVersion}/");
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", cfsLibraryOptions.CfsApiPrimaryKey);
                });
            
            services.Decorate<ICfsClient, CfsAuthorizationClientDecorator>();

            return services;
        }
    }
}
