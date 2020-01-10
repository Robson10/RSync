using System;

namespace RSync.Areas.MainWindow.ViewModels
{
    /// <summary>
    /// View model from MainWindow
    /// </summary>
    internal sealed class MainWindowVM
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
        }
    }
}