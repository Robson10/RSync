using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Localization;
using RSync.Core.Enumerations;
using RSync.Core.Extends;
using RSync.Helpers;
using RSync.Logic;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using db = RSync.Domain.Model;
namespace RSync.Areas.Settings.ViewModels
{
    /// <summary>
    /// View model for setting window.
    /// </summary>
    internal class SettingsVM : BindableBase, IWindowClose
    {
        private db.Settings settings;

        public db.Settings Settings
        {
            get => settings;
            set => SetProperty(ref settings, value);
        }

        /// <summary>
        /// Selected application language property.
        /// </summary>
        public AppLanguage SelectedAppLanguage
        {
            get => Settings.AppLanguage;
            set
            {
                Settings.AppLanguage = value;
                RaisePropertyChanged(nameof(SelectedAppLanguage));
            }
        }

        /// <summary>
        /// Save changes command.
        /// </summary>
        public DelegateCommand<Window> SaveCmd { get; private set; }

        /// <summary>
        /// Abort command.
        /// </summary>
        public DelegateCommand<Window> AbortCmd { get; private set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SettingsVM()
        {
            SaveCmd = new DelegateCommand<Window>(Save);
            AbortCmd = new DelegateCommand<Window>(Abort);
            Init();
        }

        private void Init()
        {
            db.Settings settings = SettingsLogic.GetSettings(Singleton.UserId);
            if (settings == null)
            {
                if (Singleton.UserId.HasValue)
                {
                    SettingsLogic.AddSettings(
                        new db.Settings(
                            Singleton.UserId.Value,
                            EnumUtil.GetEnumByDescription<AppLanguage>(typeof(AppLanguage), CultureInfo.CurrentCulture.Name)
                        ));
                }
            }
            Settings = SettingsLogic.GetSettings(Singleton.UserId);
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="window">Handle to close window with dialog result.</param>
        private void Save(Window window)
        {
            IsAnySettingChanged(out bool anyPropertyChanged, out bool isRestartRequaire);

            if (anyPropertyChanged)
            {
                SettingsLogic.UpdateSetings(Settings);
                if (isRestartRequaire)
                {
                    if (MessageBox.Show(res.msgSettingsRequireRestartApplication, "", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        ((IWindowClose)this).CloseWindow(window, true);
                    }
                }
                else
                {
                    ((IWindowClose)this).CloseWindow(window, true);
                }
            }
            else
            {
                ((IWindowClose)this).CloseWindow(window, false);
            }
        }

        /// <summary>
        /// Copy modified settings to singleton.
        /// Return verify result is any properties was changed and is application restart required.
        /// </summary>
        /// <param name="anyPropertyChanged">True is any property was modified.</param>
        /// <param name="restartRequired">True is property need restart application to correct working.</param>
        private void IsAnySettingChanged(out bool anyPropertyChanged, out bool restartRequired)
        {
            anyPropertyChanged = false;
            restartRequired = false;
            db.Settings previusSettings = SettingsLogic.GetSettings(Singleton.UserId);

            if (previusSettings == null)
            {
                anyPropertyChanged = true;
                restartRequired = true;
            }
            else
            {
                if (SelectedAppLanguage != previusSettings.AppLanguage)
                {
                    anyPropertyChanged = true;
                    restartRequired = true;
                }
            }
        }

        /// <summary>
        /// Abort action.
        /// </summary>
        /// <param name="window"></param>
        private void Abort(Window window)
        {
            ((IWindowClose)this).CloseWindow(window, false);
        }
    }
}