using Aircon.Data;
using Aircon.Extensions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aircon.Test
{

    public class IntegrationTestBase //: IClassFixture<AirconWebApplicationFactory>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        protected readonly AirconDbContext AirconDbContext;
        protected readonly IServiceProvider AirconServiceProvider;

        public IntegrationTestBase(AirconWebApplicationFactory factory)
        {
            _factory = factory;
            AirconServiceProvider = (IServiceProvider)factory.Services.GetService(typeof(IServiceProvider));
            AirconDbContext = _factory.Services.GetRequiredService<AirconDbContext>();
        }

        protected WebApplicationFactory<Startup> GetFactory(bool hasUser = false)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var configPath = Path.Combine(projectDir, "appsettings.json");

            return _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    conf.AddJsonFile(configPath);
                });

                builder.UseSolutionRelativeContentRoot("Aircon");

                builder.ConfigureTestServices(services =>
                {
                    services.AddMvc(options =>
                    {
                        if (hasUser)
                        {
                            options.Filters.Add(new AllowAnonymousFilter());
                            options.Filters.Add(new TestUserFilter());
                        }
                    })
                    .AddApplicationPart(typeof(Startup).Assembly);
                });
            });
        }

        protected virtual T GetService<T>()
        {
            return _factory.Services.GetService<T>();
        }

        protected virtual T GetRequiredService<T>()
        {
            return _factory.Services.GetRequiredService<T>();
        }
    }


}
