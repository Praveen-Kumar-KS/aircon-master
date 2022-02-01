using Aircon.Data;
using Aircon.Data.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class AccountTypeSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;

        public AccountTypeSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        private  List<AddressType> GetCustomerAddressType()
        {
            return new List<AddressType>
            {
                new AddressType { Name = "Warehouse", Description = "Warehouse", Active = true, IsCustomerAddressType = true },
                new AddressType { Name = "Shipping", Description = "Shipping", Active = true , IsCustomerAddressType = true},
            };
        }

        public override async Task SeedAsync()
        {
            var customerAddressTypes = GetCustomerAddressType();
            foreach (var addressType in customerAddressTypes)
            {
                var chkAddressType = _airconDbContext.AddressTypes.Where(x => x.Name == addressType.Name).SingleOrDefault();
                if (chkAddressType != null)
                {
                    chkAddressType.IsCustomerAddressType = addressType.IsCustomerAddressType;
                    _airconDbContext.AddressTypes.Update(chkAddressType);

                }
                else
                {
                    _airconDbContext.AddressTypes.Add(addressType);
                }
            }
            await _airconDbContext.SaveChangesAsync();
        }
    }

}
