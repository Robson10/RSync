using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Localization;
using RSync.Areas.Accounts.Views;
using RSync.Core.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RSync.Areas.Accounts.ViewModels
{
    /// <summary>
    /// View model used to handle account operations.
    /// </summary>
    internal class AccountsVM : BindableBase
    {
        /// <summary>
        /// Window title.
        /// </summary>
        public static string AccountsWindowTitle => res.AccountsWindowTitle;

        /// <summary>
        /// Propertis to list of pinned accounts to application.
        /// </summary>
        public List<Account> AccountsList
        {
            get
            {
                return Singleton.Accounts;
            }
            set
            {
                Singleton.Accounts = value;
                RaisePropertyChanged(nameof(AccountsList));
            }
        }

        /// <summary>
        /// Add new account command.
        /// </summary>
        public DelegateCommand AddAccountCmd { get; set; }

        /// <summary>
        /// Delete selected account command.
        /// </summary>
        public DelegateCommand DeleteAccountCmd { get; set; }

        /// <summary>
        /// Edit current account command.
        /// </summary>
        public DelegateCommand EditAccountCmd { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AccountsVM()
        {
            AddAccountCmd = new DelegateCommand(AddAccount);
        }

        private void AddAccount()
        {
            AddAccountV addAccountV = new AddAccountV();
            if (addAccountV.ShowDialog() == true)
            {
                if (addAccountV.DataContext != null
                    && (addAccountV.DataContext is AddAccountVM)
                    && (addAccountV.DataContext as AddAccountVM).Account != null)
                {
                    Account newAccount = (addAccountV.DataContext as AddAccountVM).Account;
                    AccountsList.Add(newAccount);
                    AccountsList = AccountsList.OrderBy(x => x.Server).ThenBy(x => x.CustomName).ThenBy(x => x.Login).ToList();
                }
            }
        }
    }
}
