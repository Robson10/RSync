using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace RSync.Areas.MainWindow.ViewModels
{
    internal class ToolBarVM : BindableBase
    {
        #region Properties

        #region Visibility

        private Visibility _startVisibility = Visibility.Collapsed;
        public Visibility StartVisibility { get => _startVisibility; set => SetProperty(ref _startVisibility, value); }

        private Visibility _stopVisibility = Visibility.Visible;
        public Visibility StopVisibility { get => _stopVisibility; set => SetProperty(ref _stopVisibility, value); }

        private Visibility _showAllHiddenElementsVisibility = Visibility.Visible;
        public Visibility ShowAllHiddenElementsVisibility { get => _showAllHiddenElementsVisibility; set => SetProperty(ref _showAllHiddenElementsVisibility, value); }

        private Visibility _hideAllSelectedElementsVisibility = Visibility.Collapsed;
        public Visibility HideAllSelectedElementsVisibility { get => _hideAllSelectedElementsVisibility; set => SetProperty(ref _hideAllSelectedElementsVisibility, value); }

        #endregion

        #endregion

        #region commands

        public DelegateCommand AccountsCmd { get; private set; }
        public DelegateCommand RefreshCmd { get; private set; }
        public DelegateCommand StartCmd { get; private set; }
        public DelegateCommand StopCmd { get; private set; }
        public DelegateCommand AddFileCmd { get; private set; }
        public DelegateCommand AddFolderCmd { get; private set; }
        public DelegateCommand OptionsCmd { get; private set; }
        public DelegateCommand AboutCmd { get; private set; }
        public DelegateCommand ShowAllHiddenElementsCmd { get; private set; }
        public DelegateCommand HideAllSelectedElementsCmd { get; private set; }

        #endregion

        #region Handlers

        public DelegateCommand AccountsHandler { get; set; }
        public DelegateCommand RefreshHandler { get; set; }
        public DelegateCommand StartHandler { get; set; }
        public DelegateCommand StopHandler { get; set; }
        public DelegateCommand AddFileHandler { get; set; }
        public DelegateCommand AddFolderHandler { get; set; }
        public DelegateCommand OptionsHandler { get; set; }
        public DelegateCommand AboutHandler { get; set; }
        public DelegateCommand ShowAllHiddenElementsHandler { get; set; }
        public DelegateCommand HideAllSelectedElementsHandler { get; set; }

        #endregion

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

        private void Refresh()
        {
            RefreshHandler?.Execute();
        }

        private void HideAllSelectedElements()
        {
            HideAllSelectedElementsVisibility = Visibility.Collapsed;
            ShowAllHiddenElementsVisibility = Visibility.Visible;

            HideAllSelectedElementsHandler?.Execute();
        }

        private void ShowAllHiddenElements()
        {
            ShowAllHiddenElementsVisibility = Visibility.Collapsed;
            HideAllSelectedElementsVisibility = Visibility.Visible;

            ShowAllHiddenElementsHandler?.Execute();
        }

        private void Start()
        {
            StartVisibility = Visibility.Collapsed;
            StopVisibility = Visibility.Visible;

            StartHandler?.Execute();
        }

        private void Stop()
        {
            StopVisibility = Visibility.Collapsed;
            StartVisibility = Visibility.Visible;

            StopHandler?.Execute();
        }

        private void Accounts()
        {
            AccountsHandler?.Execute();
        }

        private void Options()
        {
            OptionsHandler?.Execute();
        }

        private void About()
        {
            AboutHandler?.Execute();
        }
    }
}