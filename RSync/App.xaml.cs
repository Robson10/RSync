using RSync.AppResources.Localization;
using RSync.Core.Extends;
using RSync.Domain.Model;
using RSync.Logic;
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

#if DEBUG
            _DebugInit.Init();
#endif
        }

        /// <summary>
        /// Application and Resource(res.resx) culture is set based on language parameter.
        /// </summary>
        /// <param name="UserId"></param>
        public static void SetCurrentAppCulture(int? UserId)
        {

            if (UserId.HasValue)
            {
                Settings settings = SettingsLogic.GetSettings(UserId);
                if (settings != null)
                {
                    string localization = settings.AppLanguage.GetDescription();

                    res.Culture = new CultureInfo(localization);
                    Thread.CurrentThread.CurrentCulture = new CultureInfo(localization);
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo(localization);
                }
            }
        }
    }
}