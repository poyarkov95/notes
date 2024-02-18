using System;
using System.Collections.Generic;
using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PostgresMigrator.Migrations;

namespace PostgresMigrator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var arg = new ApplicationArguments
            {
                MigrationMode = "update"
            };
            RunApp(arg);
            // Parser.Default
            //     .ParseArguments<ApplicationArguments>(args)
            //     .WithParsed(RunApp)
            //     .WithNotParsed(ErrorArgs);
        }
        
        static void RunApp(ApplicationArguments arguments)
        {
            Console.WriteLine($"mode:{arguments.MigrationMode}");
            Console.WriteLine($"version:{arguments.MigrationVersion}");

            var serviceProvider = CreateServices();
            
            if (arguments.MigrationMode == "update")
            {
                Console.WriteLine($"update to version: {arguments.MigrationVersion}");
                
                using (serviceProvider.CreateScope())
                {
                    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                    if (arguments.MigrationVersion.HasValue)
                    {
                        runner.MigrateUp(arguments.MigrationVersion.Value);    
                    }
                    else
                    {
                        runner.MigrateUp();
                    }
                }
            }

            if (arguments.MigrationMode == "rollback")
            {
                Console.WriteLine($"rollback to version: {arguments.MigrationVersion}");
                
                using (serviceProvider.CreateScope())
                {
                    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
                    if (!arguments.MigrationVersion.HasValue)
                    {
                        Console.WriteLine("Version required for rollback");
                        return;
                    }
                    runner.RollbackToVersion(arguments.MigrationVersion.Value);
                }
            }
        }

        static void ErrorArgs(IEnumerable<Error> errs)
        {
            Console.WriteLine("Invalid application args:");
            foreach (var error in errs)
            {
                Console.WriteLine(error.Tag);
            }
        }

        private static ServiceProvider CreateServices()
        {
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json");
            var configuration = configurationBuilder.Build();
            
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithVersionTable(new VersionDatTable())
                    .WithGlobalConnectionString(configuration.GetConnectionString("notes"))
                    .ScanIn(typeof(InitMigration).Assembly).For.Migrations()
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}