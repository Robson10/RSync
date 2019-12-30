using System;
using System.Collections.Generic;
using System.Text;

namespace RSync.Areas.MainWindow.ViewModels
{
    internal sealed class MainWindowVM
    {
        public static string WindowTitle => AppDomain.CurrentDomain.FriendlyName;
        public MainWindowVM()
        {

        }
    }
}
