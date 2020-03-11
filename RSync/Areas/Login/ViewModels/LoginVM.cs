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
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System;

namespace RSync.Areas.Login.ViewModels
{
    /// <summary>
    /// Login view model.
    /// </summary>
    public class LoginVM : BindableBase, IWindowClose, IWindowVisible
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
            set
            {
                SetProperty(ref login, value);
                SetVisibilityMessageWrongCredentials(Visibility.Hidden);
            }
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
                SetVisibilityMessageWrongCredentials(Visibility.Hidden);
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
        /// Visibility for control displayed message about wrong credentials.
        /// </summary>
        private Visibility wrongCredentialsVisibility;

        /// <summary>
        /// Visibility for control displayed message about wrong credentials.
        /// </summary>
        public Visibility WrongCredentialsVisibility
        {
            get
            {
                return wrongCredentialsVisibility;
            }
            set
            {
                SetProperty(ref wrongCredentialsVisibility, value);
            }
        }

        /// <summary>
        /// Login user command.
        /// </summary>
        public DelegateCommand<Window> SignInCmd { get; set; }

        /// <summary>
        /// Abort command.
        /// </summary>
        public DelegateCommand<Window> AbortCmd { get; set; }

        /// <summary>
        /// Add user account command.
        /// </summary>
        public DelegateCommand<Window> AddAccountCmd { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public LoginVM()
        {
            SignInCmd = new DelegateCommand<Window>(SignIn);
            AbortCmd = new DelegateCommand<Window>(Abort);
            AddAccountCmd = new DelegateCommand<Window>(AddAccount);
#if DEBUG
            Login = "Harry";
            Password = "123";
#endif
        }

        /// <summary>
        /// Open window to adding new user.
        /// </summary>
        private void AddAccount(Window window)
        {
            ((IWindowVisible)this).HideWindow(window, Visibility.Hidden);
            AddAccountV addAccount = new AddAccountV();
            addAccount.ShowDialog();
            ((IWindowVisible)this).HideWindow(window, Visibility.Visible);
        }

        /// <summary>
        /// Abort user logging.
        /// </summary>
        private void Abort(Window window)
        {
            ((IWindowClose)this).CloseWindow(window, false);
        }

        /// <summary>
        /// Logging user to application.
        /// </summary>
        private void SignIn(Window window)
        {
            User user = UserLogic.GetUser(Login, Password);

            if (user != null)
            {
                if (!ValidateUserRsaKeys(user))
                {
                    return;
                }

                Singleton.UserId = user.UserId;
                App.SetCurrentAppCulture(user.UserId);

                ((IWindowClose)this).CloseWindow(window, true);
            }
            else
            {
                SetVisibilityMessageWrongCredentials(Visibility.Visible);
            }
        }

        /// <summary>
        /// Validate is user RSA keys are correct.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <returns>Is RSA private and public key are correct.</returns>
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

        /// <summary>
        /// Copy user public RSA key to application singleton class.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Validate RSA private key or create it if not exist after user approve.
        /// </summary>
        /// <param name="user">User object.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Create new Rsa Keys.
        /// </summary>
        /// <param name="user"></param>
        private void CrateNewRsaKeys(User user)
        {
            user.SerializedRsaParameter = RsaHelper.CreateRsaKeys();
            UserLogic.UpdateUser(user);
            RemoveOldEncryptedData();
        }

        /// <summary>
        /// Remove old data from database related to old RSA keys.
        /// </summary>
        private void RemoveOldEncryptedData()
        {
            //TODO:Implement removing old encrypted data.
        }

        /// <summary>
        /// Set visibility of control containing message about wrong credentials.
        /// </summary>
        /// <param name="visibility">Control visibility.</param>
        private void SetVisibilityMessageWrongCredentials(Visibility visibility)
        {
            if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
            {
                if (WrongCredentialsVisibility != Visibility.Hidden)
                {
                    WrongCredentialsVisibility = Visibility.Hidden;
                }
            }
            else
            {
                WrongCredentialsVisibility = Visibility.Visible;
            }
        }
    }
}
