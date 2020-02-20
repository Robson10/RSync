using Prism.Commands;
using Prism.Mvvm;
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

                IsCredencialsCorrect = !(string.IsNullOrEmpty(ErrorLogin) || string.IsNullOrEmpty(ErrorPassword));

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
                string decryptedPassword = RsaHelper.Decrypt(password);
                return decryptedPassword;
            }
            set
            {
                string encryptedPassword = RsaHelper.Encrypt(value, Singleton.RsaPublicKey);
                SetProperty(ref password, encryptedPassword);
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
        public DelegateCommand AddCmd { get; set; }

        /// <summary>
        /// Abort command.
        /// </summary>
        public DelegateCommand AbortCmd { get; set; }

        /// <summary>
        /// Command invoked on close window.
        /// </summary>
        public DelegateCommand<WindowResult> CloseHandle { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AddAccountVM()
        {
            AddCmd = new DelegateCommand(Add);
            AbortCmd = new DelegateCommand(Abort);
        }

        private void Add()
        {
            if (string.IsNullOrEmpty(Login))
            {
                MessageBox.Show(res.msgEmptyLogin, string.Empty, MessageBoxButton.OK);
            }

            if (UserLogic.GetUser(Login) is object)
            {
                MessageBox.Show(res.msgLoginInUse, string.Empty, MessageBoxButton.OK);
            }

            if (string.IsNullOrEmpty(Password))
            {
                MessageBox.Show(res.msgEmptyPassword, string.Empty, MessageBoxButton.OK);
            }
            User user = new User(Login, Password, RsaHelper.CreateRsaKeys());
            UserLogic.AddUser(user);
            CloseHandle?.Execute(WindowResult.Ok);
        }

        private void Abort()
        {
            CloseHandle?.Execute(WindowResult.Abort);
        }
    }
}
