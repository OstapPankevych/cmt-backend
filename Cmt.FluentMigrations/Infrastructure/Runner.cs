using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace Cmt.FluentMigrations.Infrastructure
{
    public class Runner
    {
        private readonly string _identityConn;
        private readonly string _cmtConn;

        public Runner(
            string identityConnectionString,
            string cmtConnectionString)
        {
            _identityConn = identityConnectionString;
            _cmtConn = cmtConnectionString;
        }

        public IMigrationRunner GetIdentityMigrations()
        {
            return CreateMigrationRunner(_identityConn, new[] { Db.Identity });
        }

        public IMigrationRunner GetCmtMigrations()
        {
            return CreateMigrationRunner(_cmtConn, new[] { Db.Cmt });
        }

        private IMigrationRunner CreateMigrationRunner(string connectionString, string[] tags)
        {
            var serviceProvider = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .Configure<FluentMigratorLoggerOptions>(cfg => {
                    cfg.ShowElapsedTime = true;
                    cfg.ShowSql = true;
                })
                .Configure<RunnerOptions>(opt =>
                {
                    opt.Tags = tags;
                })
                .BuildServiceProvider(false);

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            return runner;
        }
    }
}
