using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using RSync.AppResources.Configuration;
using RSync.AppResources.Localization;
using RSync.Areas.Login.Views;
using RSync.Core.Exceptions;
using RSync.Core.Helpers;
using RSync.Domain.Model;
using RSync.Helpers;
using RSync.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace RSync.Areas.Login.ViewModels
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginVM : BindableBase
    {
        /// <summary>
        /// User login to application.
        /// </summary>
        private string login;

        /// <summary>
        /// User login to application.
        /// </summary>
        public string Login
        {
            get { return login; }
            set { SetProperty(ref login, value); }
        }

        /// <summary>
        /// Encrypted user password to application.
        /// </summary>
        private string password;

        /// <summary>
        /// User password to application
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
        /// Window visibility.
        /// </summary>
        private Visibility windowVisibility = Visibility.Visible;

        /// <summary>
        /// Window visibility.
        /// </summary>
        public Visibility WindowVisibility
        {
            get => windowVisibility;
            set => SetProperty(ref windowVisibility, value);
        }

        /// <summary>
        /// Login user command.
        /// </summary>
        public DelegateCommand SignInCmd { get; set; }

        /// <summary>
        /// Abort command.
        /// </summary>
        public DelegateCommand AbortCmd { get; set; }

        /// <summary>
        /// Command invoked to close application from called class.
        /// </summary>
        public ICommand CloseApplicationHandler { get; set; }

        /// <summary>
        /// Command invoked after log in.
        /// </summary>
        public ICommand LoginSuccesHandler { get; set; }

        /// <summary>
        /// Add user account command.
        /// </summary>
        public DelegateCommand AddAccountCmd { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginVM()
        {
            SignInCmd = new DelegateCommand(SignIn);
            AbortCmd = new DelegateCommand(Abort);
            AddAccountCmd = new DelegateCommand(AddAccount);
#if DEBUG
            Login = "Harry";
            Password = "123";
#endif
        }

        /// <summary>
        /// Open window to adding new user.
        /// </summary>
        private void AddAccount()
        {
            WindowVisibility = Visibility.Hidden;
            AddAccountV addAccount = new AddAccountV();
            addAccount.ShowDialog();
            WindowVisibility = Visibility.Visible;
        }

        /// <summary>
        /// Abort user logging.
        /// </summary>
        private void Abort()
        {
            if (CloseApplicationHandler != null)
                CloseApplicationHandler.Execute(null);
        }

        /// <summary>
        /// Logging user to application.
        /// </summary>
        private void SignIn()
        {
            User user = UserLogic.GetUser(Login, Password);

            if (user is object)
            {
                if (!ValidateUserRsaKeys(user))
                {
                    return;
                }

                Singleton.UserId = user.UserId;
                App.SetCurrentAppCulture(user.UserId);
                if (LoginSuccesHandler != null)
                {
                    LoginSuccesHandler.Execute(null);
                }
            }
        }

        private bool ValidateUserRsaKeys(User user)
        {
            if (!ValidatePrivateKey(user))
            {
                return false;
            }
            if (!CopyPublicKey(user))
            {
                return false;
            }

            if (!RsaHelper.IsKeysCorrect(Singleton.RsaPublicKey))
            {
                if (MessageBoxResult.Yes == MessageBox.Show(
                           string.Format(res.msgRsaKeysNotMatch, AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.YesNo))
                {
                    CrateNewRsaKeys(user);
                }
            }

            return true;
        }

        private bool CopyPublicKey(User user)
        {
            if (user != null)
            {
                try
                {
                    Singleton.RsaPublicKey = user.GetRsaPublicKey();
                    return true;
                }
                catch (PropertyNullException ex)
                {
                    LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                    if (MessageBoxResult.Yes == MessageBox.Show(
                            string.Format(res.msgPublicRsaNotFound, AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.YesNo))
                    {
                        CrateNewRsaKeys(user);
                    }
                }
                catch (JsonSerializationException ex)
                {
                    LogHelper.LogError(ex, CultureInfo.CurrentCulture);
                    if (MessageBoxResult.Yes == MessageBox.Show(
                            string.Format(res.msgCannotDeserializePublicRsaKey, AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.YesNo))
                    {
                        CrateNewRsaKeys(user);
                    }
                }
            }
            return false;
        }

        private bool ValidatePrivateKey(User user)
        {
            if (user != null)
            {
                try
                {
                    if (RsaHelper.IsRsaPrivateKeyExist())
                    {
                        return true;
                    }
                    else
                    {
                        if (MessageBoxResult.Yes == MessageBox.Show(
                            string.Format(res.msgPrivateRsaNotFound, AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.YesNo))
                        {
                            CrateNewRsaKeys(user);
                        }
                    }
                }
                catch (JsonSerializationException ex)
                {
                    LogHelper.LogError(ex, CultureInfo.CurrentCulture);

                    if (MessageBoxResult.Yes == MessageBox.Show(
                        string.Format(res.msgCannotDeserializePrivateRsaKey, AppPaths.RsaPrivateKeyFilePath), res.capError, MessageBoxButton.YesNo))
                    {
                        CrateNewRsaKeys(user);
                    }
                }
            }
            return false;
        }

        private void CrateNewRsaKeys(User user)
        {
            user.SerializedRsaParameter= RsaHelper.CreateRsaKeys();
            UserLogic.UpdateUser(user);
            RemoveOldEncryptedData();
        }

        private void RemoveOldEncryptedData()
        {
            //TODO:Implement removing old encrypted data.
        }
    }
}
