using Offlogs.Client.Tests.Fakers;
using OffLogs.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.AspNetCore3
{
    [Collection("Api.Frontend")]
    public class AspNetCore3BaseTest : IClassFixture<AspNetCore3WebApplicationFactory>, IDisposable
    {
        protected readonly AspNetCore3WebApplicationFactory _factory;
        protected readonly FakeHttpClient _offlogsHttpClient;

        public AspNetCore3BaseTest(AspNetCore3WebApplicationFactory factory)
        {
            _factory = factory;
            _offlogsHttpClient = _factory.Services.GetService(typeof(IHttpClient)) as FakeHttpClient;
        }

        public void Dispose()
        {
            _offlogsHttpClient.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
