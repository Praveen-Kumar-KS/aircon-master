using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public interface ISystemDataContributor
    {
        Task SeedData();
    }
    public class SystemDataContributor : ISystemDataContributor
    {

        private readonly IEnumerable<IDataSeeder> _dataSeeders;

        public SystemDataContributor(IEnumerable<IDataSeeder> dataSeeders)
        {
            _dataSeeders = dataSeeders.Where(x=> x.SeedType == SeedDataType.System).OrderBy(x=> x.Order);
        }
        public async Task SeedData()
        {
            foreach(var seeder in _dataSeeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
