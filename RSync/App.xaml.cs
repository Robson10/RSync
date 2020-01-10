using RSync.AppResources.Localization;
using RSync.Core.Classes;
using RSync.Core.Enumerations;
using RSync.Core.Extends;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace RSync
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Startup method.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetCurrentAppCulture(Singleton.Settings.AppLanguage);
        }

        /// <summary>
        /// Application and Resource(res.resx) culture is set based on language parameter.
        /// </summary>
        /// <param name="language"></param>
        public static void SetCurrentAppCulture(AppLanguage language)
        {
            string localization = language.GetDescription();

            res.Culture = new CultureInfo(localization);
            Thread.CurrentThread.CurrentCulture = new CultureInfo(localization);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(localization);
        }
    }
}