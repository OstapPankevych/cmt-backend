using System.IO;
using Cmt.FluentMigrations.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace Cmt.FluentMigrations
{
    class Program
    {
        private const string config = "appsettings.json";

        private const string CmtDb = "CmtDatabase";
        private const string IdentityDb = "CmtIdentityDatabase";

        private static readonly IConfigurationRoot _configuration;

        static Program()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(config, true, true);

            _configuration = builder.Build();
        }

        static void Main(string[] args)
        {
            var runner = new Runner(
                GetConnectionString(IdentityDb),
                GetConnectionString(CmtDb));

            runner.GetCmtMigrations().MigrateUp();
            runner.GetIdentityMigrations().MigrateUp();
        }

        private static string GetConnectionString(string sectionName)
            => _configuration.GetConnectionString(sectionName);
    }
}
