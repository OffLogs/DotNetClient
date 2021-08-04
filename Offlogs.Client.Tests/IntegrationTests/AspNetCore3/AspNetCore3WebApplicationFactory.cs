using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Offlogs.Client.Tests.Extensions;
using OffLogs.Client;
using Offlogs.Client.Tests.Fakers;
using Offlogs.Client.TestApp.AspNetCore3;
using System.Reflection;
using OffLogs.Client.AspNetCore;

namespace Offlogs.Client.Tests.IntegrationTests.AspNetCore3
{
    public class AspNetCore3WebApplicationFactory : WebApplicationFactory<AspNetCore3TestStartup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // We can further customize our application setup here.
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IOffLogsHttpClient));
                services.Remove(descriptor);
                services.AddSingleton<IOffLogsHttpClient, FakeHttpClient>();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // This method should be here to run the tests
        }

        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureLogging(logging =>
                        {
                            logging.AddOffLogsLogger();
                        })
                        .ConfigureTestServices(services =>
                        {
                            
                            // We can further customize our application setup here.
                        })
                        .ConfigureAppConfiguration(builder =>
                        {
                            builder.ConfigureConfigurationProvider();
                        })
                        .UseTestServer();
                });
            return builder;
        }
    }
}
