using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Localization;
using RSync.Areas.MainWindow.Views;
using RSync.Core.Helpers;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Threading;

namespace RSync.Areas.MainWindow.ViewModels
{
    /// <summary>
    /// View model from MainWindow
    /// </summary>
    internal sealed class MainWindowVM : BindableBase
    {
        /// <summary>
        /// Sign in command.
        /// </summary>
        public DelegateCommand SignInCmd { get; set; }

        /// <summary>
        /// Tool bar view model.
        /// </summary>
        public ToolBarVM ToolBarVM { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MainWindowVM(ToolBarV toolBarV)
        {
            if(toolBarV ==null || toolBarV.DataContext == null)
            {
                ArgumentNullException ex = new ArgumentNullException(res.exArgumentNullException);
                LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                throw ex;
            }

            if (toolBarV.DataContext is ToolBarVM toolBarVM)
            {
                ToolBarVM = toolBarVM;
            }
            else
            {
                MessageBox.Show(res.exDataContextTypeException, res.capError, MessageBoxButton.OK);
                InvalidCastException ex = new InvalidCastException(string.Format(res.exDataContextTypeException, typeof(ToolBarVM), typeof(ToolBarV)));
                LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                throw ex;
            }

            SignInCmd = new DelegateCommand(SignIn);
        }

        private void SignIn()
        {
            ToolBarVM.SignInCmd?.Execute();
        }
    }
}
