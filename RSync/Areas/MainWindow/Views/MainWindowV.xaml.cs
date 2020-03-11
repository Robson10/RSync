using RSync.Areas.MainWindow.ViewModels;
using System.Windows;

namespace RSync.Areas.MainWindow.Views
{
    /// <summary>
    /// Interaction logic for MainWindowV.xaml
    /// </summary>
    public partial class MainWindowV : Window
    {
        /// <summary>
        /// Default MainWindow view constructor.
        /// </summary>
        public MainWindowV()
        {
            InitializeComponent();
            DataContext = new MainWindowVM(ToolBarV);
        }
    }
}