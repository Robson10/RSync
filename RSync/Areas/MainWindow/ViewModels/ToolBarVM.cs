using Prism.Commands;
using Prism.Mvvm;
using RSync.Areas.Accounts.Views;
using RSync.Areas.Settings.Views;
using System.Windows;

namespace RSync.Areas.MainWindow.ViewModels
{
    /// <summary>
    /// View model for ToolBar
    /// </summary>
    public class ToolBarVM : BindableBase
    {
        #region Properties

        #region Visibility

        /// <summary>
        /// Visibility of start button.
        /// </summary>
        private Visibility _startVisibility = Visibility.Collapsed;

        /// <summary>
        /// Visibility property of start button.
        /// </summary>
        public Visibility StartVisibility { get => _startVisibility; set => SetProperty(ref _startVisibility, value); }

        /// <summary>
        /// Visibility of stop button.
        /// </summary>
        private Visibility _stopVisibility = Visibility.Visible;

        /// <summary>
        /// Visibility property of stop button.
        /// </summary>
        public Visibility StopVisibility { get => _stopVisibility; set => SetProperty(ref _stopVisibility, value); }

        /// <summary>
        /// Visibility of "show all hidden elements" button.
        /// </summary>
        private Visibility _showAllHiddenElementsVisibility = Visibility.Visible;

        /// <summary>
        /// Visibility property of "show all hidden elements" button.
        /// </summary>
        public Visibility ShowAllHiddenElementsVisibility { get => _showAllHiddenElementsVisibility; set => SetProperty(ref _showAllHiddenElementsVisibility, value); }

        /// <summary>
        /// Visibility of "hide all selected elements" button.
        /// </summary>
        private Visibility _hideAllSelectedElementsVisibility = Visibility.Collapsed;

        /// <summary>
        /// Visibility property of "hide all selected elements" button.
        /// </summary>
        public Visibility HideAllSelectedElementsVisibility { get => _hideAllSelectedElementsVisibility; set => SetProperty(ref _hideAllSelectedElementsVisibility, value); }

        #endregion Visibility

        #endregion Properties

        #region commands

        /// <summary>
        /// Accounts management command.
        /// </summary>
        public DelegateCommand AccountsCmd { get; private set; }

        /// <summary>
        /// Command used to refresh list files on server or synchronize files skipping interval.
        /// </summary>
        public DelegateCommand RefreshCmd { get; private set; }

        /// <summary>
        /// Command used to start synchronization.
        /// </summary>
        public DelegateCommand StartCmd { get; private set; }

        /// <summary>
        /// Command used to stop synchronization.
        /// </summary>
        public DelegateCommand StopCmd { get; private set; }

        /// <summary>
        /// Command used to add a file to list of files to synchronized.
        /// </summary>
        public DelegateCommand AddFileCmd { get; private set; }

        /// <summary>
        /// command used to add folder to list of files to synchronize.
        /// </summary>
        public DelegateCommand AddFolderCmd { get; private set; }

        /// <summary>
        /// Command used to open options window.
        /// </summary>
        public DelegateCommand OptionsCmd { get; private set; }

        /// <summary>
        /// Command used to open about window.
        /// </summary>
        public DelegateCommand AboutCmd { get; private set; }

        /// <summary>
        /// Command used to show all elements selected by user to hidden.
        /// </summary>
        public DelegateCommand ShowAllHiddenElementsCmd { get; private set; }

        /// <summary>
        /// Command used to hide all elements selected by user to hide.
        /// </summary>
        public DelegateCommand HideAllSelectedElementsCmd { get; private set; }

        #endregion commands

        #region Handlers

        /// <summary>
        /// Handler invoked by AccountsCmd on completed default code.
        /// </summary>
        public DelegateCommand AccountsHandler { get; set; }

        /// <summary>
        /// Handler invoked by RefreshCmd on completed default code.
        /// </summary>
        public DelegateCommand RefreshHandler { get; set; }

        /// <summary>
        /// Handler invoked by StartCmd on completed default code.
        /// </summary>
        public DelegateCommand StartHandler { get; set; }

        /// <summary>
        /// Handler invoked by StopCmd on completed default code.
        /// </summary>
        public DelegateCommand StopHandler { get; set; }

        /// <summary>
        /// Handler invoked by AddFileCmd on completed default code.
        /// </summary>
        public DelegateCommand AddFileHandler { get; set; }

        /// <summary>
        /// Handler invoked by AddFolderCmd on completed default code.
        /// </summary>
        public DelegateCommand AddFolderHandler { get; set; }

        /// <summary>
        /// Handler invoked by OptionsCmd on completed default code.
        /// </summary>
        public DelegateCommand OptionsHandler { get; set; }

        /// <summary>
        /// Handler invoked by AboutCmd on completed default code.
        /// </summary>
        public DelegateCommand AboutHandler { get; set; }

        /// <summary>
        /// Handler invoked by ShowAllHidenElementsCmd on completed default code.
        /// </summary>
        public DelegateCommand ShowAllHiddenElementsHandler { get; set; }

        /// <summary>
        /// Handler invoked by HideAllSelectedElementsCmd on completed default code.
        /// </summary>
        public DelegateCommand HideAllSelectedElementsHandler { get; set; }

        #endregion Handlers

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ToolBarVM()
        {
            RefreshCmd = new DelegateCommand(Refresh);
            StartCmd = new DelegateCommand(Start);
            StopCmd = new DelegateCommand(Stop);
            AccountsCmd = new DelegateCommand(Accounts);
            OptionsCmd = new DelegateCommand(Options);
            AboutCmd = new DelegateCommand(About);
            ShowAllHiddenElementsCmd = new DelegateCommand(ShowAllHiddenElements);
            HideAllSelectedElementsCmd = new DelegateCommand(HideAllSelectedElements);
        }

        /// <summary>
        /// Refresh action method.
        /// </summary>
        private void Refresh()
        {
            RefreshHandler?.Execute();
        }

        /// <summary>
        /// Hide all selected elements action method.
        /// Change button visibility.
        /// </summary>
        private void HideAllSelectedElements()
        {
            HideAllSelectedElementsVisibility = Visibility.Collapsed;
            ShowAllHiddenElementsVisibility = Visibility.Visible;

            HideAllSelectedElementsHandler?.Execute();
        }

        /// <summary>
        /// Show all hidden elements action method.
        /// Change buttons visibility.
        /// </summary>
        private void ShowAllHiddenElements()
        {
            ShowAllHiddenElementsVisibility = Visibility.Collapsed;
            HideAllSelectedElementsVisibility = Visibility.Visible;

            ShowAllHiddenElementsHandler?.Execute();
        }

        /// <summary>
        /// Start action method. Hide start button and show stop button.
        /// </summary>
        private void Start()
        {
            StartVisibility = Visibility.Collapsed;
            StopVisibility = Visibility.Visible;

            StartHandler?.Execute();
        }

        /// <summary>
        /// Stop action method. Hide stop button and show start button.
        /// </summary>
        private void Stop()
        {
            StopVisibility = Visibility.Collapsed;
            StartVisibility = Visibility.Visible;

            StopHandler?.Execute();
        }

        /// <summary>
        /// Accounts action method. Show window to manage accounts.
        /// </summary>
        private void Accounts()
        {
            AccountsV accountsV = new AccountsV();
            if(accountsV.ShowDialog() ==true)
            {

            }

            AccountsHandler?.Execute();
        }

        /// <summary>
        /// Options action method. Show window to change application settings.
        /// </summary>
        private void Options()
        {
            SettingsV settingsV = new SettingsV();

            if (settingsV.ShowDialog() == true)
            {
            }

            OptionsHandler?.Execute();
        }

        /// <summary>
        /// About action method. Show window with informations about application.
        /// </summary>
        private void About()
        {
            AboutHandler?.Execute();
        }
    }
}