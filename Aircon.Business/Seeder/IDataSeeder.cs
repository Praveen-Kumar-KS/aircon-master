using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public interface IDataSeeder
    {
        Task SeedAsync();
        string SeedType { get; }
        int Order { get; }
    }

    public abstract class BaseDataSeeder : IDataSeeder
    {

        public abstract string SeedType { get; }

        public abstract int Order { get; }

        public abstract Task SeedAsync();
    }
}
