using Prism.Commands;
using Prism.Mvvm;
using RSync.Areas.Login.ViewModels;
using RSync.Areas.Login.Views;
using System;

namespace RSync.Areas.MainWindow.ViewModels
{
    /// <summary>
    /// View model from MainWindow
    /// </summary>
    internal sealed class MainWindowVM : BindableBase
    {
        /// <summary>
        /// Property to display application title
        /// </summary>
        public static string WindowTitle => AppDomain.CurrentDomain.FriendlyName;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindowVM()
        {
            LoginV loginV = new LoginV();
            if (loginV.DataContext != null)
            {
                (loginV.DataContext as LoginVM).LoginSuccesHandler = new DelegateCommand(() => loginV.Close());
                (loginV.DataContext as LoginVM).CloseApplicationHandler = new DelegateCommand(CloseApp);
            }

            if (loginV.ShowDialog() == false)
            {
            }
        }
        public void CloseApp()
        {
            try
            {
                App.Current.Shutdown();
            }
            catch { }
        }
    }
}
