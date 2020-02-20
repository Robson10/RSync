using RSync.AppResources.Localization;
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
        public static string SettingsFilePath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", ConfigurationFolderPath, config.SettingsFileName);
                CreateFileIfNotExist(path);

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
                CreateFolderIfNotExist(path);

                return path;
            }
        }

        /// <summary>
        /// Path to Data folder. Create folder if not exist.
        /// </summary>
        public static string DataFolderPath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", AppPath, config.DataFolderName);
                CreateFolderIfNotExist(path);

                return path;
            }
        }

        /// <summary>
        /// Path to file containing RSA private key. Create empty file if not exist.
        /// </summary>
        public static string RsaPrivateKeyFilePath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", ConfigurationFolderPath, config.RsaPrivateKeyFileName);
                CreateFileIfNotExist(path);

                return path;
            }
        }

        /// <summary>
        /// Path to file containing error logs. Create empty file if not exist.
        /// </summary>
        public static string LogFilePath
        {
            get
            {
                string path = string.Format(@"{0}\{1}", DataFolderPath, config.LogFileName);
                
                bool isExist = CreateFileIfNotExist(path);
                
                if(!isExist)
                {
                    File.WriteAllText(path, res.ErrorLogHeaders);
                }

                return path;
            }
        }

        /// <summary>
        /// Verify if folder exist. If not exist create new empty file.
        /// </summary>
        /// <param name="path"></param>
        private static bool CreateFolderIfNotExist(string path)
        {
            bool isExist = Directory.Exists(path);

            if (!isExist)
            {
                Directory.CreateDirectory(path);
            }

            return isExist;
        }

        /// <summary>
        /// Verify if file exist. If not exist create new empty file.
        /// </summary>
        /// <param name="path">Full path to expected file</param>
        private static bool CreateFileIfNotExist(string path)
        {
            bool isExist = File.Exists(path);
            
            if (!isExist)
            {
                File.Create(path).Close();
            }
            
            return isExist;

        }
    }
}