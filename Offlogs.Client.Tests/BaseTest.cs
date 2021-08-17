using Microsoft.Extensions.Configuration;
using Offlogs.Client.Tests.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offlogs.Client.Tests
{
    public class BaseTest : IDisposable
    {
        protected static IConfiguration Configuration;

        protected string ApiToken
        {
            get => Configuration.GetValue<string>("OffLogs:ApiToken");
        }

        static BaseTest()
        {
            Configuration = TestUtils.BuildConfiguration();
        }

        public BaseTest()
        { 
            
        }

        public void Dispose()
        {
            
        }
    }
}
