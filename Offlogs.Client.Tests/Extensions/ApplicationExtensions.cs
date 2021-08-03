using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Offlogs.Client.Tests.Extensions
{
    public static class ApplicationExtensions
    {
        public static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .ConfigureConfigurationProvider()
                .Build();
        }

        public static IConfigurationBuilder ConfigureConfigurationProvider(this IConfigurationBuilder builder)
        {
            return builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.Local.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
