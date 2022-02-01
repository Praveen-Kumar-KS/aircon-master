using System.IO;

namespace Aircon.Helper
{
    public static class Paths
    {
        public static string KeyFile
        {
            get
            {
                return Path.Combine(Paths.MetadataDir, "k");
            }
        }

        public static string SettingsFile
        {
            get
            {
                return Path.Combine(Paths.MetadataDir, "s");
            }
        }

        public static string MetadataDir
        {
            get
            {
                return Path.Combine(Config.BaseDir, "m");
            }
        }



        internal static void EnsureNecessaryDirectoriesArePresent()
        {
            EnsureDirectoryExists(MetadataDir);
            //EnsureDirectoryExists(KeyFile);
        }

        private static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
