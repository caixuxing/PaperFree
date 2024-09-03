using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Trasen.PaperFree.DbMigrator
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.AddAppSettingsSecretsJson()
                //.ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }
}