using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Localization;
using RSync.Core.Classes;
using RSync.Core.Enumerations;
using System.Diagnostics;
using System.Windows;

namespace RSync.Areas.Settings.ViewModels
{
    /// <summary>
    /// View model for setting window.
    /// </summary>
    internal class SettingsVM : BindableBase
    {
        /// <summary>
        /// Selected application language.
        /// </summary>
        private AppLanguage _selectedAppLanguage = Singleton.Settings.AppLanguage;

        /// <summary>
        /// Selected application language property.
        /// </summary>
        public AppLanguage SelectedAppLanguage
        {
            get => _selectedAppLanguage;
            set => SetProperty(ref _selectedAppLanguage, value);
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
                Singleton.Settings.SaveChanges();
                if (isRestartRequaire)
                {
                    if (MessageBox.Show(res.msgSettingsRequireRestartApplication, "", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                    {
                        Process.Start(Process.GetCurrentProcess().MainModule.FileName);
                        Application.Current.Shutdown();
                    }
                    else
                    {
                        CloseWindow(window, true);
                    }
                }
                else
                {
                    CloseWindow(window, true);
                }
            }
            else
            {
                CloseWindow(window, false);
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

            if (Singleton.Settings.AppLanguage != SelectedAppLanguage)
            {
                Singleton.Settings.AppLanguage = SelectedAppLanguage;

                anyPropertyChanged = true;
                restartRequired = true;
            }
        }

        /// <summary>
        /// Abort action.
        /// </summary>
        /// <param name="window"></param>
        private void Abort(Window window)
        {
            CloseWindow(window, false);
        }

        /// <summary>
        /// Close window with given dialog result.
        /// </summary>
        /// <param name="window">Handle to current window.</param>
        /// <param name="dialogResult">Value for dialog result property in window.</param>
        private void CloseWindow(Window window, bool dialogResult)
        {
            if (window != null)
            {
                window.DialogResult = dialogResult;
                window.Close();
            }
        }
    }
}