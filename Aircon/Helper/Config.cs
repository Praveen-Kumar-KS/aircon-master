using System.IO;

namespace Aircon.Helper
{
    public static class Config
    {
        public static string BaseDir
        {
            get
            {
                return Path.Combine("Aircon", "config");
            }
        }
    }

}
