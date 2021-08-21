using Microsoft.Extensions.Configuration;
using Offlogs.Client.TestApp.AspNetCore3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offlogs.Client.Tests.IntegrationTests.AspNetCore3
{
    public class AspNetCore3TestStartup: Startup
    {
        public AspNetCore3TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
