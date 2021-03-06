﻿using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Localization;
using RSync.Core.Classes;
using RSync.Core.Enumerations;
using RSync.Core.Security;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace RSync.Areas.Accounts.ViewModels
{
    /// <summary>
    /// View model of Add Account view.
    /// </summary>
    public class AddAccountVM : BindableBase, IDataErrorInfo
    {
        #region DataErrorInfo

        /// <summary>
        /// Error message for login.
        /// </summary>
        private string errorLogin = "";

        /// <summary>
        /// Error message for password.
        /// </summary>
        private string errorPassword = "";

        /// <summary>
        /// Error message for password.
        /// </summary>
        private string errorCustomName = "";

        /// <summary>
        /// Property to error message displayed on error during Login control validation.
        /// </summary>
        public string ErrorLogin
        {
            get => errorLogin;
            set => SetProperty(ref errorLogin, value);
        }

        /// <summary>
        /// Property to error message displayed on error during password control validation.
        /// </summary>
        public string ErrorPassword
        {
            get => errorPassword;
            set => SetProperty(ref errorPassword, value);
        }

        /// <summary>
        /// Property to error message displayed on error during CustomName control validation.
        /// </summary>
        public string ErrorCustomName
        {
            get => errorCustomName;
            set => SetProperty(ref errorCustomName, value);
        }

        /// <summary>
        /// Default error property for IDataErrorInfo interface
        /// </summary>
        public string Error => string.Empty;

        /// <summary>
        /// Validation properties.
        /// </summary>
        /// <param name="propertyName">Name of property to validate.</param>
        /// <returns></returns>
        public string this[string propertyName]
        {
            get
            {
                string result = string.Empty;
                switch (propertyName)
                {
                    case nameof(SelectedServer):
                        {
                            if (Singleton.Accounts.Any(x => x.Server == SelectedServer && x.Login.Equals(Login)))
                            {
                                ErrorLogin = res.msgAccountExist;
                            }
                            else if(ErrorLogin.Equals(res.msgAccountExist))
                            {
                                ErrorLogin = string.Empty;
                            }
                        }
                        break;
                    case nameof(Login):
                        {
                            if (string.IsNullOrEmpty(Login))
                            {
                                result = res.msgEmptyLogin;
                            }
                            else if (Singleton.Accounts.Any(x => x.Server==SelectedServer && x.Login.Equals(Login)))
                            {
                                result = res.msgAccountExist;
                            }

                            ErrorLogin = result;
                        }
                        break;
                    case nameof(Password):
                        {
                            if (string.IsNullOrEmpty(Password))
                            {
                                result = res.msgEmptyPassword;
                            }

                            ErrorPassword = result;
                        }
                        break;
                    case nameof(CustomName):
                        {
                            if (string.IsNullOrEmpty(CustomName))
                            {
                                result = res.msgEmptyCustomName;
                            }
                            else if (Singleton.Accounts.Any(x => x.CustomName.Equals(CustomName))) 
                            {
                                result = res.msgCustomNameInUse;
                            }

                            ErrorCustomName = result;
                        }
                        break;
                }

                TestConnectionIsEnabled = string.IsNullOrEmpty(ErrorLogin) && string.IsNullOrEmpty(ErrorPassword);

                return result;
            }
        }

        #endregion DataErrorInfo

        /// <summary>
        /// Login value.
        /// </summary>
        private string login;

        /// <summary>
        /// Encrypted password.
        /// </summary>
        private string password;

        /// <summary>
        /// Custom name for account
        /// </summary>
        private string customName;

        /// <summary>
        /// Selected server.
        /// </summary>
        private ServerType selectedServer;

        /// <summary>
        /// Is enabled button TestConnection.
        /// </summary>
        private bool testConnectionIsEnabled = false;

        /// <summary>
        /// Message about connection result.
        /// </summary>
        private string testConnectionMessage;

        /// <summary>
        /// Is connection to server completed successfully.
        /// </summary>
        private bool isTestConnectionConnected;

        /// <summary>
        /// Complete and verified account object. Null if user abort adding account.
        /// </summary>
        public Account Account = null;

        /// <summary>
        /// Property containing value used as login in server credentials.
        /// </summary>
        public string Login
        {
            get => login;
            set
            {
                if (string.IsNullOrEmpty(CustomName) || (!string.IsNullOrEmpty(Login) && Login.Equals(CustomName)))
                {
                    CustomName = value;
                }
                SetProperty(ref login, value);
                IsCredentialsCorrect = false;
            }
        }

        /// <summary>
        /// Property containing value used as password as server credentials.
        /// </summary>
        public string Password
        {
            get
            {
                string decryptedPassword = RsaCryptographyHelper.Decrypt(password);
                return decryptedPassword;
            }
            set
            {
                string encryptedPassword = RsaCryptographyHelper.Encrypt(value);
                SetProperty(ref password, encryptedPassword);
                IsCredentialsCorrect = false;
            }
        }

        /// <summary>
        /// Custom name for account given by user.
        /// </summary>
        public string CustomName
        {
            get => customName;
            set => SetProperty(ref customName, value);
        }

        /// <summary>
        /// Current selected server.
        /// </summary>
        public ServerType SelectedServer
        {
            get => selectedServer;
            set => SetProperty(ref selectedServer, value);
        }

        /// <summary>
        /// Is enabled TestConnection button.
        /// </summary>
        public bool TestConnectionIsEnabled
        {
            get => testConnectionIsEnabled;
            set => SetProperty(ref testConnectionIsEnabled, value);
        }

        /// <summary>
        /// Message about connection result.
        /// </summary>
        public string TestConnectionMessage
        {
            get => testConnectionMessage;
            set => SetProperty(ref testConnectionMessage, value);

        }

        /// <summary>
        /// Is connection to server completed successfully.
        /// </summary>
        public bool IsCredentialsCorrect
        {
            get => isTestConnectionConnected;
            set => SetProperty(ref isTestConnectionConnected, value);
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
        /// Test connection command.
        /// </summary>
        public DelegateCommand TestConnectionCmd { get; private set; }
        
        /// <summary>
        /// Default constructor of adding account view model.
        /// </summary>
        public AddAccountVM()
        {
            SaveCmd = new DelegateCommand<Window>(Save);
            AbortCmd = new DelegateCommand<Window>(Abort);
            TestConnectionCmd = new DelegateCommand(TestConnection);
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        /// <param name="window">Handle to close window with dialog result.</param>
        private void Save(Window window)
        {
            Account = null;
            if (!string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(CustomName))
            {
                Account = new Account(Login, Password, CustomName, SelectedServer, false);
            }

            CloseWindow(window, true);
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

        /// <summary>
        /// Credentials verification by test connection to server.
        /// </summary>
        private void TestConnection()
        {
            //TODO: implement connection/credentials validation
            string msgConnectionTest;
            bool isCredentialsCorrect = true;

            if (!isCredentialsCorrect)
            {
                msgConnectionTest = res.errNotImplementedException;
                MessageBox.Show(msgConnectionTest);
            }
            else
            {
                MessageBox.Show(res.errNotImplementedException);
                msgConnectionTest = res.msgTestConnectionConnected;
            }

            IsCredentialsCorrect = isCredentialsCorrect;
            TestConnectionMessage = msgConnectionTest;
        }
    }
}
