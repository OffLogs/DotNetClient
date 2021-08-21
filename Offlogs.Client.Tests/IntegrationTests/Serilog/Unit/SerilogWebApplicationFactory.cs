using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OffLogs.Client;
using OffLogs.Client.AspNetCore;
using Offlogs.Client.TestApp.AspNetCore3;
using Offlogs.Client.Tests.Core;
using Offlogs.Client.Tests.Extensions;
using Offlogs.Client.Tests.Fakers;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OffLogs;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit
{
    public class AspNetCore3WebApplicationFactory : WebApplicationFactory<AspNetCore3.AspNetCore3TestStartup>
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
            var configuration = TestUtils.BuildConfiguration();

            using var log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.OffLogs(configuration, LogEventLevel.Verbose, new FakeStaticHttpClient())
                .CreateLogger();
            
            var builder = Host.CreateDefaultBuilder()
                .UseSerilog(log)
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Startup>()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .ConfigureTestServices(services =>
                        {
                            
                            // We can further customize our application setup here.
                        })
                        .ConfigureAppConfiguration(configurationBuilder =>
                        {
                            configurationBuilder.ConfigureConfigurationProvider();
                        })
                        .UseTestServer();
                });
            return builder;
        }
    }
}
