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
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyIsImtpZCI6Ii1LSTNROW5OUjdiUm9meG1lWm9YcWJIWkdldyJ9.eyJhdWQiOiJjMzE2M2JmMS0wOTJmLTQzNmItYjI2MC03YWRlNTk3M2U1YjkiLCJpc3MiOiJodHRwczovL3N0cy53aW5kb3dzLm5ldC83MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDcvIiwiaWF0IjoxNjc1NDE5NDY4LCJuYmYiOjE2NzU0MTk0NjgsImV4cCI6MTY3NTQyNDc2MywiYWNyIjoiMSIsImFpbyI6IkFWUUFxLzhUQUFBQTBGNUdpeVJDRFVOenFoSEhFZ1h1OVg0VGphSkRNcXBtTTFySDBPRml6MkxTY05QeEM4V01iN3dUSFBqYWNHZE9EWXRoL0VuVnc2SXloRUxvYTV1aGJ0VWo4Q3RkVEQ2ZjhUa0NmYktVVmpjPSIsImFtciI6WyJyc2EiLCJtZmEiXSwiYXBwaWQiOiJkYTg2OWM2ZS1lMjlkLTQ0NWItOGEzNC0xNDRjMTAxZDk2YmQiLCJhcHBpZGFjciI6IjAiLCJkZXZpY2VpZCI6ImRjNzE2OGI5LWFhMGQtNGQ1MS1iOTEzLTk2ODdmODY5M2ViNSIsImZhbWlseV9uYW1lIjoiTWVpa29wb3Vsb3MiLCJnaXZlbl9uYW1lIjoiT3Jlc3RpcyIsImlwYWRkciI6IjE5NC4yMTkuNjAuMTMwIiwibmFtZSI6Ik9yZXN0aXMgTWVpa29wb3Vsb3MiLCJvaWQiOiI5ZWFkN2NiMy01ZjAyLTRkZjItYWExNS1jYzI4MDNiN2I5MzYiLCJvbnByZW1fc2lkIjoiUy0xLTUtMjEtMTcyMTI1NDc2My00NjI2OTU4MDYtMTUzODg4MjI4MS0zOTQ0ODI3IiwicHVpZCI6IjEwMDNCRkZEOUYwODY1OUYiLCJyaCI6IjAuQVJvQXY0ajVjdkdHcjBHUnF5MTgwQkhiUl9FN0ZzTXZDV3REc21CNjNsbHo1YmthQVA0LiIsInNjcCI6IlVzZXIuUmVhZCIsInN1YiI6Im1ucHcxM29xbXZtQVdsdUZ4YmY4aU1KTEsxRi1WRGtrZ3JUVk5sTEd1eWsiLCJ0aWQiOiI3MmY5ODhiZi04NmYxLTQxYWYtOTFhYi0yZDdjZDAxMWRiNDciLCJ1bmlxdWVfbmFtZSI6Im9ybWVpa29wQG1pY3Jvc29mdC5jb20iLCJ1cG4iOiJvcm1laWtvcEBtaWNyb3NvZnQuY29tIiwidXRpIjoiSlFuSm1Rbkg0VWktQTBPcFZ6djFBQSIsInZlciI6IjEuMCJ9.URLkoeYg-ns8PtUKF0_Pn4-rNptqF19P9MAH51t35Xr26eaZN02KbPshZrzUfbd3CaAAp_mc983ct9SQudaSt6VD0opLXgl3QN4r0Q2YujMt6cPEyG_I4KIDOBw-el_nnUsq9Q6Lj5QYPpWvtidc6mmLtckDupcSD8uh6O2owpKkntgk3_Fqz6cYgxWk_Zrqej3uqcATsbW8GwyPXkLedsd03i_TDvKM-l7-Ufip7JYJeeyo2Slj7ykVi1SkvJeUqB8pPLJRXrIlOxG3bi9i91-7-MV5POgZu-7sLWGcHSdBSZNQN_XybBl1KO1pvqbHKmuKcznhPa90qUFdKAJvMw");
                });

            return services;
        }
    }
}
