using System;
using System.Threading.Tasks;
using Aircon.Data;
using Aircon.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Xunit;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vg.Common.Notification.Message;
using Aircon.Business.Seeder;

namespace Aircon.Test
{

    public class AirconWebApplicationFactory : WebApplicationFactory<Startup>,  IDisposable
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            //var connectionString = "Data Source=C:\\Sqlite\\testdb.db";
            var connectionString = "Data Source=:memory:;";
            _sqliteConnection = CreateDatabaseAndGetConnection(connectionString);
            builder.UseEnvironment("Testing").ConfigureServices(services =>
            {
                services.AddEntityFrameworkSqlite()
                    .AddDbContext<AirconDbContext>(options =>
                    {
                        options.UseSqlite(_sqliteConnection);
                    });
                services.AddScoped<IEmailSender, TestSmtpEmailSender>();
            });
            
        }

        public AirconWebApplicationFactory()
        {
            var seedSystem = Services.GetRequiredService<ISystemDataContributor>();
            seedSystem.SeedData().GetAwaiter().GetResult(); 
        }

        private SqliteConnection _sqliteConnection;

        private SqliteConnection CreateDatabaseAndGetConnection(string connectionString)
        {
            var connection = new SqliteConnection(connectionString);
            connection.Open();

            var options = new DbContextOptionsBuilder<AirconDbContext>()
                .UseSqlite(connection)
                .Options;

            using (var context = new AirconDbContext(options))
            {
                context.GetService<IRelationalDatabaseCreator>().CreateTables();
            }

            return connection;
        }

        public new void  Dispose()
        {
            
            _sqliteConnection.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _sqliteConnection.Close();
            }
            // Call the base Dispose, to release resources on the base class.
            base.Dispose(disposing);
        }
    }
}
