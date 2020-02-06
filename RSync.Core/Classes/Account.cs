using RSync.Core.Enumerations;
using RSync.Core.Security;

namespace RSync.Core.Classes
{
    public class Account
    {
        /// <summary>
        /// Login.
        /// </summary>
        private string login;

        /// <summary>
        /// Encrypted password.
        /// </summary>
        private string password;

        /// <summary>
        /// Custom name given by user for account.
        /// </summary>
        private string customName;

        /// <summary>
        /// Server to which account belongs.
        /// </summary>
        private ServerType server = ServerType.Google;

        /// <summary>
        /// Is account suspended.
        /// </summary>
        private bool isSuspended;

        /// <summary>
        /// Text used as login in server credentials.
        /// </summary>
        public string Login
        {
            get => login;
            set => login = value;
        }

        /// <summary>
        /// Decrypted text used as password in server credentials.
        /// </summary>
        public string Password
        {
            get
            {
                var decryptedPassword = RsaCryptographyHelper.Decrypt(password);
                return decryptedPassword;
            }
            set
            {
                string encryptedPassword = RsaCryptographyHelper.Encrypt(value);
                password = encryptedPassword;
            }
        }

        /// <summary>
        /// Name given by user for account.
        /// </summary>
        public string CustomName
        {
            get => customName;
            set => customName = value;
        }

        /// <summary>
        /// Server to which account belongs.
        /// </summary>
        public ServerType Server
        {
            get => server;
            set => server = value;
        }

        /// <summary>
        /// Is account suspended.
        /// </summary>
        public bool IsSuspended
        {
            get => isSuspended;
            set => isSuspended = value;
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="login">Parameter used as login during login to the server.</param>
        /// <param name="password">Parameter used as password during login to the server.</param>
        /// <param name="customName">Custom name given by user for account.</param>
        /// <param name="server">Server to which account belongs.</param>
        /// <param name="isSuspended">Is account suspended.</param>
        public Account(string login, string password, string customName, ServerType server, bool isSuspended)
        {
            Login = login;
            Password = password;
            CustomName = customName;
            Server = server;
            IsSuspended = isSuspended;
        }
    }
}
