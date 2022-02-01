using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core.Configuration
{
    public partial class AppSettings
    {
        public CacheConfig CacheConfig { get; set; } = new CacheConfig();

        public HostingConfig HostingConfig { get; set; } = new HostingConfig();

    }
}
