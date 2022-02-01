using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Core.Configuration
{
    public partial class CacheConfig : IConfig
    {
        /// <summary>
        /// Gets or sets the default cache time in minutes
        /// </summary>
        public int DefaultCacheTime { get; set; } = 60;

        /// <summary>
        /// Gets or sets the short term cache time in minutes
        /// </summary>
        public int ShortTermCacheTime { get; set; } = 3;

        /// <summary>
        /// Gets or sets the bundled files cache time in minutes
        /// </summary>
        public int BundledFilesCacheTime { get; set; } = 120;
    }
}
