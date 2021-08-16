using System;
using OffLogs.Client;
using Offlogs.Client.Tests.Fakers;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog
{
    [Collection("Api.Frontend")]
    public class AspNetCore3BaseTest : IClassFixture<AspNetCore3WebApplicationFactory>, IDisposable
    {
        protected readonly AspNetCore3WebApplicationFactory _factory;
        protected readonly FakeHttpClient _offlogsHttpClient;

        public AspNetCore3BaseTest(AspNetCore3WebApplicationFactory factory)
        {
            _factory = factory;
            _offlogsHttpClient = _factory.Services.GetService(typeof(IOffLogsHttpClient)) as FakeHttpClient;
        }

        public void Dispose()
        {
            _offlogsHttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
