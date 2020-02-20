using Newtonsoft.Json;
using RSync.AppResources.Localization;
using RSync.Core.Converters;
using RSync.Core.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace RSync.Domain.Model
{
    /// <summary>
    /// User table.
    /// </summary>
    [Table(nameof(User))]
    public class User
    {
        /// <summary>
        /// User id.
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// RSAParameter containing RSA public key serialized to JSon
        /// </summary>
        public string SerializedRsaParameter { get; set; }

        public virtual Settings Settings { get; set; }

        /// <summary>
        /// Default constructor for new user.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="password">User password.</param>
        /// <param name="serializedRsaParameter">Serialized RsaParameter containing public RSA key.</param>
        public User(string login, string password, string serializedRsaParameter)
        {
            Login = login;
            Password = password;
            this.SerializedRsaParameter = serializedRsaParameter;
        }

        /// <summary>
        /// Deserialized property SerializedRsaParameter.
        /// </summary>
        /// <exception cref="PropertyNullException">The exception thrown when SerializedRsaParameter is null or empty.</exception>
        /// <exception cref="JsonSerializationException">The exception thrown when cannot serialize to expected object.</exception>
        /// <returns>RsaParameter containing public key.</returns>
        public RSAParameters GetRsaPublicKey()
        {
            if (string.IsNullOrEmpty(SerializedRsaParameter))
            {
                throw new PropertyNullException(string.Format(res.exPropertyNullException,nameof(SerializedRsaParameter)));
            }

            return ObjectToJson.Deserialize<RSAParameters>(SerializedRsaParameter);
        }
    }
}
