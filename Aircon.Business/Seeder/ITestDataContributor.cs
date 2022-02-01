using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public interface ITestDataContributor
    {
        Task SeedData();
    }
    public class TestDataContributor : ITestDataContributor
    {

        private readonly IEnumerable<IDataSeeder> _dataSeeders;

        public TestDataContributor(IEnumerable<IDataSeeder> dataSeeders)
        {
            _dataSeeders = dataSeeders.Where(x => x.SeedType == SeedDataType.Test).OrderBy(x => x.Order).ToList();
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
