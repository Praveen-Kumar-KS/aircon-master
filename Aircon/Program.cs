using System.Linq;
using System.Threading.Tasks;
using Aircon.Data;
using Aircon.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Aircon.Business.Seeder;

namespace Aircon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //This migrates the database and adds any seed data as required
            await SetupDatabasesAndSeedAsync(host);
            await host.RunAsync();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((context, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration);
                });

        private static async Task SetupDatabasesAndSeedAsync(IHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                using (var context = services.GetRequiredService<AirconDbContext>())
                {
                    if (context.Database.GetPendingMigrations().ToList().Count > 0)
                    {
                        context.Database.Migrate();
                    }
                }

                //var seedSystem = services.GetRequiredService<ISystemDataContributor>();
                //await seedSystem.SeedData();

                //var seedDemo = services.GetRequiredService<IDemoDataContributor>();
                //await seedDemo.SeedData();

                //await webHost.Services.CheckRolesAndPermissionsAsync();
                //await webHost.Services.CheckAddSystemAdminSetupAsync();
                //await webHost.Services.CheckAddAccountTypesSetupAsync();
                //await webHost.Services.CheckAddDemoSeedAsync();
                //await webHost.Services.CheckAddSampleAirconUsersAsync();
                //await webHost.Services.CheckAddSampleUsersAsync();
                //await webHost.Services.CheckAddDemoUsersAsync();
                //await webHost.Services.CheckAddSampleCustoerAndOpportunitysAsync();


            }

            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var seedSystem = services.GetRequiredService<ISystemDataContributor>();
                await seedSystem.SeedData();
                var seedDemo = services.GetRequiredService<IDemoDataContributor>();
                await seedDemo.SeedData();
            }
        }

    }
}
