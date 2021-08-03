using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Offlogs.Client.Tests.IntegrationTests.AspNetCore3.Tests
{
    public class SendSimpleLogsTests : AspNetCore3BaseTest
    {
        public SendSimpleLogsTests(AspNetCore3WebApplicationFactory factory) : base(factory)
        {
        }

        [Theory]
        [InlineData("/simple/info")]
        public async Task ShouldSendSimpleInfoLog(string url)
        { 

        }
    }
}
