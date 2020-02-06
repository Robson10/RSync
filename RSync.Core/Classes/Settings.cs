using Prism.Mvvm;
using RSync.AppResources.Configuration;
using RSync.Core.Converters;
using RSync.Core.Enumerations;
using RSync.Core.Extends;
using System.Globalization;
using System.IO;

namespace RSync.Core.Classes
{
    /// <summary>
    /// Class containing all application settings
    /// </summary>
    public class Settings : BindableBase
    {
        /// <summary>
        /// Application language
        /// </summary>
        private AppLanguage _appLanguage;

        /// <summary>
        /// Application language property
        /// </summary>
        public AppLanguage AppLanguage
        {
            get => _appLanguage; set => SetProperty(ref _appLanguage, value);
        }

        /// <summary>
        /// Default class constructor.
        /// </summary>
        private Settings()
        {
        }

        /// <summary>
        /// Create filled settings object based on file with settings.
        /// </summary>
        /// <returns></returns>
        public static Settings Initialize()
        {
            Settings settings = null;
            string readText = File.ReadAllText(AppPaths.SettingsFilePath);

            if (!string.IsNullOrEmpty(readText))
            {
                settings = ObjectToJson.Deserialize<Settings>(readText);
            }

            if (settings == default(Settings))
            {
                settings = new Settings
                {
                    AppLanguage = EnumUtil.GetEnumByDescription<AppLanguage>(typeof(AppLanguage), CultureInfo.CurrentCulture.Name)
                };

                SaveChanges(settings);
            }

            return settings;
        }

        /// <summary>
        /// Save current state of Singleton.Settings properties to file.
        /// </summary>
        public void SaveChanges()
        {
            SaveChanges(this);
        }

        /// <summary>
        /// Save settings object to file. Do nothing when parameter is null.
        /// </summary>
        /// <param name="settings">Setting object</param>
        private static void SaveChanges(Settings settings)
        {
            if (settings == null)
            {
                return;
            }

            string json = ObjectToJson.Serialize(settings);
            File.WriteAllText(AppPaths.SettingsFilePath, json);
        }
    }
}