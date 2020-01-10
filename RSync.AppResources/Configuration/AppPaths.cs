using System.IO;
using System.Reflection;

namespace RSync.AppResources.Configuration
{
    /// <summary>
    /// All paths used in project with data support.
    /// </summary>
    public static class AppPaths
    {
        /// <summary>
        /// Path to application folder.
        /// </summary>
        private static readonly string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Path to application folder.
        /// </summary>
        public static string AppPath => appPath;

        /// <summary>
        /// Path to settings file. Create empty file if not exist.
        /// </summary>
        public static string SettingsPath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", ConfigurationFolderPath, config.SettingsFileName);

                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }

                return path;
            }
        }

        /// <summary>
        /// Path to configuration folder. Create folder if not exist.
        /// </summary>
        public static string ConfigurationFolderPath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", AppPath, config.ConfigFolderName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
    }
}