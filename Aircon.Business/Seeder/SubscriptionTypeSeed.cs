using Aircon.Data;
using Aircon.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class SubscriptionTypeSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;

        public SubscriptionTypeSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            foreach (var type in _airconDbContext.SubscriptionTypes)
            {
                type.Active = false;
                _airconDbContext.SubscriptionTypes.Update(type);
            }
            _airconDbContext.SaveChanges();
            var subscription1 = new SubscriptionType
            {
                IsPopular = true,
                Line1 = "2 YEARS",
                Line2 = "24 x 7 Live Support",
                Line3 = "Consolidated Rates ",
                Line4 = "Savings of up-to 60% per shipment",
                Line5 = "Cloud Access",
                Active = true,
                Name = "2 YEARS",
                MonthlyAmount = 900,
                AnnualAmount = 10800,
                DisplayOrder = 1
            };
            var subscription2 = new SubscriptionType
            {
                IsPopular = false,
                Line1 = "1 YEAR",
                Line2 = "24 x 7 Live Support",
                Line3 = "Consolidated Rates ",
                Line4 = "Savings of up-to 60% per shipment",
                Line5 = "Cloud Access",
                Active = true,
                Name = "1 YEAR",
                MonthlyAmount = 1000,
                AnnualAmount = 12000,
                DisplayOrder = 2
            };
            var subscription3 = new SubscriptionType
            {
                IsPopular = false,
                Line1 = "MONTH TO MONTH",
                Line2 = "24 x 7 Live Support",
                Line3 = "Consolidated Rates ",
                Line4 = "Savings of up-to 60% per shipment",
                Line5 = "Cloud Access",
                Active = true,
                Name = "MONTH TO MONTH",
                MonthlyAmount = 1500,
                AnnualAmount = 18000,
                DisplayOrder = 3
            };
            var s1 = _airconDbContext.SubscriptionTypes.Where(x => x.Name == subscription1.Name).SingleOrDefault();
            if (s1 != null)
            {
                s1.IsPopular = subscription1.IsPopular;
                s1.Line1 = subscription1.Line1;
                s1.Line2 = subscription1.Line2;
                s1.Line3 = subscription1.Line3;
                s1.Line4 = subscription1.Line4;
                s1.Line5 = subscription1.Line5;
                s1.Active = subscription1.Active;
                s1.MonthlyAmount = subscription1.MonthlyAmount;
                s1.AnnualAmount = subscription1.AnnualAmount;
                s1.DisplayOrder = subscription1.DisplayOrder;
                _airconDbContext.SubscriptionTypes.Update(s1);
            }
            else
            {
                _airconDbContext.SubscriptionTypes.Add(subscription1);
            }

            var s2 = _airconDbContext.SubscriptionTypes.Where(x => x.Name == subscription2.Name).SingleOrDefault();
            if (s2 != null)
            {
                s2.IsPopular = subscription2.IsPopular;
                s2.Line1 = subscription2.Line1;
                s2.Line2 = subscription2.Line2;
                s2.Line3 = subscription2.Line3;
                s2.Line4 = subscription2.Line4;
                s2.Line5 = subscription2.Line5;
                s2.Active = subscription2.Active;
                s2.MonthlyAmount = subscription2.MonthlyAmount;
                s2.AnnualAmount = subscription2.AnnualAmount;
                s2.DisplayOrder = subscription2.DisplayOrder;
                _airconDbContext.SubscriptionTypes.Update(s2);
            }
            else
            {
                _airconDbContext.SubscriptionTypes.Add(subscription2);
            }

            var s3 = _airconDbContext.SubscriptionTypes.Where(x => x.Name == subscription3.Name).SingleOrDefault();
            if (s3 != null)
            {
                s3.IsPopular = subscription3.IsPopular;
                s3.Line1 = subscription3.Line1;
                s3.Line2 = subscription3.Line2;
                s3.Line3 = subscription3.Line3;
                s3.Line4 = subscription3.Line4;
                s3.Line5 = subscription3.Line5;
                s3.Active = subscription3.Active;
                s3.MonthlyAmount = subscription3.MonthlyAmount;
                s3.AnnualAmount = subscription3.AnnualAmount;
                s3.DisplayOrder = subscription3.DisplayOrder;
                _airconDbContext.SubscriptionTypes.Update(s3);
            }
            else
            {
                _airconDbContext.SubscriptionTypes.Add(subscription3);
            }

            await _airconDbContext.SaveChangesAsync();


        }
    }

}
