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
        private readonly AspNetCore3WebApplicationFactory _factory;

        public AspNetCore3BaseTest(AspNetCore3WebApplicationFactory factory)
        {
            _factory = factory;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
