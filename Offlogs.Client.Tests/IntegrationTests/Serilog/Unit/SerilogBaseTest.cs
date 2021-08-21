using System;
using OffLogs.Client;
using Offlogs.Client.Tests.Fakers;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.Serilog.Unit
{
    [Collection("Api.Frontend")]
    public class SerilogBaseTest : IClassFixture<AspNetCore3WebApplicationFactory>, IDisposable
    {
        protected readonly AspNetCore3WebApplicationFactory _factory;

        public SerilogBaseTest(AspNetCore3WebApplicationFactory factory)
        {
            _factory = factory;
        }

        public void Dispose()
        {
            FakeStaticHttpClient.Clear();
            GC.SuppressFinalize(this);
        }
    }
}
