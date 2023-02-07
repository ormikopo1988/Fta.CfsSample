using Azure.CfS.Library;
using Fta.CfsSample.Api.Settings;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(Fta.CfsSample.Api.Startup))]
namespace Fta.CfsSample.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                 .SetBasePath(Environment.CurrentDirectory)
                 .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                 .AddEnvironmentVariables()
                 .Build();

            var azureAdSettings = new AzureAdSettings();
            configuration.Bind(AzureAdSettings.AzureAdSectionKey, azureAdSettings);
            builder.Services.AddSingleton(azureAdSettings);

            var cfsApiSettings = new CfsApiSettings();
            configuration.Bind(CfsApiSettings.CfsApiSectionKey, cfsApiSettings);
            builder.Services.AddSingleton(cfsApiSettings);

            builder.Services.AddCfsLibrary(configuration.GetValue<string>("CfsApi:BaseUrl"), configuration.GetValue<string>("CfsApi:PrimaryKey"));
        }
    }
}
