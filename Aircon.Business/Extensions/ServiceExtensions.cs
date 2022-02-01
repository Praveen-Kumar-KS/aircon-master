using Aircon.Business.Seeder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSeed(this IServiceCollection services)
        {
            services.AddScoped<IDemoDataContributor, DemoDataContributor>();
            services.AddScoped<ITestDataContributor, TestDataContributor>();
            services.AddScoped<ISystemDataContributor, SystemDataContributor>();
            services.AddScoped<IDataSeeder, AccountTypeSeed>();
            services.AddScoped<IDataSeeder, CountrySeed>();
            services.AddScoped<IDataSeeder, CustomerDemoSeed>();
            services.AddScoped<IDataSeeder, DefaultPreferenceSeed>();
            services.AddScoped<IDataSeeder, EmployeeDemoSeed>();
            services.AddScoped<IDataSeeder, FreeDomainsSeed>();
            services.AddScoped<IDataSeeder, RolesAndPermissionsSeed>();
            services.AddScoped<IDataSeeder, SubscriptionTypeSeed>();
            services.AddScoped<IDataSeeder, SystemUserSeed>();
            services.AddScoped<IDataSeeder, TemplateDefinitionSeed>();
            services.AddScoped<IDataSeeder, AirportSeed>();
            services.AddScoped<IDataSeeder, QuoteSeed>();
            services.AddScoped<IDataSeeder, BookingSeed>();
            services.AddScoped<IDataSeeder, ShipmentInformationSeed>();
            services.AddScoped<IDataSeeder, QuotePricingSeed>();
            services.AddScoped<IDataSeeder, ShipmentInformationDetailSeed>();
            //services.AddScoped<IDataSeeder, BookingAddressSeed>();
            //services.AddScoped<IDataSeeder, BookingDocumentSeed>();
            //services.AddScoped<IDataSeeder, BookingNotificationSeed>();
            return services;
        }

    }
}
