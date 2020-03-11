using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Configuration;
using RSync.AppResources.Localization;
using RSync.Core.Enumerations;
using RSync.Core.Helpers;
using RSync.Domain.Model;
using RSync.Helpers;
using RSync.Logic;
using System.ComponentModel;
using System.Windows;

namespace RSync.Areas.Login.ViewModels
{
    /// <summary>
    /// Add account view model.
    /// </summary>
    public class AddAccountVM : BindableBase, IDataErrorInfo, IWindowClose
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
                    case nameof(Login):
                        {
                            if (string.IsNullOrEmpty(Login))
                            {
                                result = res.msgEmptyLogin;
                            }
                            else if (UserLogic.GetUser(login) != null)
                            {
                                result = res.msgLoginInUse;
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
                }

                IsCredencialsCorrect = string.IsNullOrEmpty(ErrorLogin) && string.IsNullOrEmpty(ErrorPassword);

                return result;
            }
        }

        #endregion DataErrorInfo

        /// <summary>
        /// Login value.
        /// </summary>
        private string login;

        /// <summary>
        /// Property containing value used as login in server credentials.
        /// </summary>
        public string Login
        {
            get => login;
            set => SetProperty(ref login, value);
        }

        /// <summary>
        /// Encrypted password.
        /// </summary>
        private string password;

        /// <summary>
        /// Property containing value used as password as server credentials.
        /// </summary>
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                SetProperty(ref password, value);
            }
        }

        /// <summary>
        /// Is login and password correct.
        /// </summary>
        private bool isCredencialsCorrect = false;

        /// <summary>
        /// Is login and password correct.
        /// </summary>
        public bool IsCredencialsCorrect
        {
            get => isCredencialsCorrect;
            set => SetProperty(ref isCredencialsCorrect, value);
        }

        /// <summary>
        /// Add account command.
        /// </summary>
        public DelegateCommand<Window> AddCmd { get; set; }

        /// <summary>
        /// Abort command.
        /// </summary>
        public DelegateCommand<Window> AbortCmd { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddAccountVM()
        {
            AddCmd = new DelegateCommand<Window>(Add);
            AbortCmd = new DelegateCommand<Window>(Abort);
        }

        private void Add(Window window)
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show(res.msgEmptyLogin, string.Empty, MessageBoxButton.OK);
            }
            else if (UserLogic.GetUser(Login) is object)
            {
                MessageBox.Show(res.msgLoginInUse, string.Empty, MessageBoxButton.OK);
            }
            else if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show(res.msgEmptyPassword, string.Empty, MessageBoxButton.OK);
            }
            else if (RsaHelper.IsRsaPrivateKeyExist())
            {
                MessageBox.Show(string.Format(res.msgCannotCreateNewUser,AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.OK);
            }
            else
            {
                User user = new User(Login, Password, RsaHelper.CreateRsaKeys());
                UserLogic.AddUser(user);
                ((IWindowClose)this).CloseWindow(window, true);
            }
        }

        private void Abort(Window window)
        {
            ((IWindowClose)this).CloseWindow(window, false);
        }
    }
}
