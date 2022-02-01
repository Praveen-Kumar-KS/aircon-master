using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public interface IDemoDataContributor
    {
        Task SeedData();
    }
    public class DemoDataContributor : IDemoDataContributor
    {

        private readonly IEnumerable<IDataSeeder> _dataSeeders;

        public DemoDataContributor(IEnumerable<IDataSeeder> dataSeeders)
        {
            _dataSeeders = dataSeeders.Where(x => x.SeedType == SeedDataType.Demo).OrderBy(x => x.Order).ToList();
        }
        public async Task SeedData()
        {
            foreach (var seeder in _dataSeeders)
            {
                await seeder.SeedAsync();
            }
        }
    }
}
