using RSync.Core.Enumerations;
using RSync.Core.Helpers;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace RSync.Domain.Model
{
    /// <summary>
    /// Server object.
    /// </summary>
    [Table(nameof(Server))]
    public class Server
    {
        /// <summary>
        /// Primary key Servers table.
        /// </summary>
        [Key]
        public int ServerId { get; set; }

        /// <summary>
        /// Foreign key to User.UserId
        /// </summary>
        [ForeignKey(nameof(User.UserId))]
        public int UserId { get; set; }

        /// <summary>
        /// Text used as login in server credentials.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Encrypted text used as password in server credentials.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Name given by user for account.
        /// </summary>
        public string CustomName { get; set; }

        /// <summary>
        /// Server to which account belongs.
        /// </summary>
        [Column(nameof(ServerType), TypeName = "tinyint")]
        public ServerType ServerType { get; set; }

        /// <summary>
        /// Is account suspended.
        /// </summary>
        public bool IsSuspended { get; set; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="login">Parameter used as login during login to the server.</param>
        /// <param name="password">Parameter used as password during login to the server.</param>
        /// <param name="customName">Custom name given by user for account.</param>
        /// <param name="serverType">Server to which account belongs.</param>
        /// <param name="isSuspended">Is account suspended.</param>
        public Server(string login, string password, string customName, ServerType serverType, bool isSuspended)
        {
            Login = login;
            Password = password;
            CustomName = customName;
            ServerType = serverType;
            IsSuspended = isSuspended;
        }

        /// <summary>
        /// Decrypt Password property.
        /// </summary>
        /// <exception cref="Exception">Throw if RSA private key is empty.</exception>
        /// <returns>Decrypted Password property</returns>
        public string DecryptPassword()
        {
            return RsaHelper.Decrypt(Password);
        }

        /// <summary>
        /// Set Password property encrypted password parameter.
        /// </summary>
        /// <param name="rsaParameters">RSAParameter containing public RSA key.</param>
        /// <param name="password">User password to encrypt.</param>
        public void EncryptPassword(RSAParameters rsaParameters, string password)
        {
            Password = RsaHelper.Encrypt(password, rsaParameters);
        }
    }
}
