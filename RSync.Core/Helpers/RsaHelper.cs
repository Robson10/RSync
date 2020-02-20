using Newtonsoft.Json;
using RSync.AppResources.Configuration;
using RSync.AppResources.Localization;
using RSync.Core.Converters;
using RSync.Core.Exceptions;
using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSync.Core.Helpers
{
    /// <summary>
    /// Used to encrypt/decrypt text data.
    /// </summary>
    public static class RsaHelper
    {
        /// <summary>
        /// RSA byte count.
        /// </summary>
        private const int RsaKeySize = 4096;

        /// <summary>
        /// Read RSA public and private key from file or create new even if one of keys not exist.
        /// </summary>
        /// <exception cref="FileEmptyException">The exception thrown when file with RSA key is empty.</exception>
        /// <exception cref="JsonReaderException">The exception thrown on deserialization reading.</exception>
        /// <exception cref="JsonSerializationException">The exception thrown when cannot serialize to expected object.</exception>
        /// <exception cref="ArgumentNullException">The exception thrown when argument is null.</exception>
        /// <returns>RSAParameter containing RSA private key.</returns>
        private static RSAParameters ReadRsaPrivateKey()
        {
            string rsaPrivateKeyJson;
            try
            {
                rsaPrivateKeyJson = File.ReadAllText(AppPaths.RsaPrivateKeyFilePath);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(ex,CultureInfo.CurrentCulture);
                rsaPrivateKeyJson = string.Empty;
            }

            if (string.IsNullOrEmpty(rsaPrivateKeyJson))
            {
                throw new FileEmptyException(string.Format(CultureInfo.CurrentCulture, res.msgPrivateRsaNotFound, AppPaths.RsaPrivateKeyFilePath));
            }

            return ObjectToJson.Deserialize<RSAParameters>(rsaPrivateKeyJson);
        }

        /// <summary>
        /// Encrypt plain text.
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <returns></returns>
        public static string Encrypt(string plainText, RSAParameters rsaPublicKey)
        {
            string cipherText = string.Empty;
            byte[] bytesCypherText;

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
            {
                csp.ImportParameters(rsaPublicKey);
                byte[] bytesPlainTextData = Encoding.Unicode.GetBytes(plainText);
                //apply pkcs#1.5 padding and encrypt our data 
                bytesCypherText = csp.Encrypt(bytesPlainTextData, false);
            }

            if (bytesCypherText != null && bytesCypherText.Length > 0)
            {
                cipherText = Convert.ToBase64String(bytesCypherText);
            }

            return cipherText;
        }

        /// <summary>
        /// Decrypt cipher text into plaint text
        /// </summary>
        /// <param name="cipherText">Cipher text</param>
        /// <exception cref="FileEmptyException">The exception thrown file with RSA key is empty.</exception>
        /// <exception cref="JsonReaderException">The exception thrown on deserialization reading.</exception>
        /// <exception cref="JsonSerializationException">The exception thrown when cannot serialize to expected object.</exception>
        /// <exception cref="ArgumentNullException">The exception thrown when argument is null.</exception>
        /// <returns>Decrypted text</returns>
        public static string Decrypt(string cipherText)
        {
            RSAParameters rsaPrivateKey = ReadRsaPrivateKey();

            string plainTextData = string.Empty;
            byte[] bytesPlainTextData;
            byte[] bytesCypherText = Convert.FromBase64String(cipherText);

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
            {
                csp.ImportParameters(rsaPrivateKey);
                //decrypt and strip pkcs#1.5 padding
                bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            }

            if (bytesPlainTextData != null && bytesPlainTextData.Length > 0)
            {
                plainTextData = Encoding.Unicode.GetString(bytesPlainTextData);
            }

            return plainTextData;
        }

        /// <summary>
        /// Create local file with private RSA key and return public RSA key.
        /// </summary>
        /// <returns>RSA public key.</returns>
        public static string CreateRsaKeys()
        {
            string rsaPrivateKeyJson;
            string rsaPublicKeyJson;

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
            {
                rsaPrivateKeyJson = csp.ExportParameters(true).Serialize();
                rsaPublicKeyJson = csp.ExportParameters(false).Serialize();
                csp.Dispose();
            }

            File.WriteAllText(AppPaths.RsaPrivateKeyFilePath, rsaPrivateKeyJson);

            return rsaPublicKeyJson;
        }

        /// <summary>
        /// Validate is RSA private key exist.
        /// </summary>
        /// <exception cref="JsonSerializationException">The exception thrown when private key cannot be deserialized.</exception>
        /// <returns>True is validation result work correctly.</returns>
        public static bool IsRsaPrivateKeyExist()
        {
            try
            {
                ReadRsaPrivateKey();
                return true;
            }
            catch(JsonSerializationException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Validate is keys are compatible.
        /// </summary>
        /// <param name="publicRsaKey">Public RSA key</param>
        /// <exception cref="FileEmptyException">The exception thrown when file with RSA key is empty.</exception>
        /// <returns>True if example text after encrypt and decrypt are equals to original value.</returns>
        public static bool IsKeysCorrect(RSAParameters publicRsaKey)
        {
            string encryptedText = Encrypt(res.testText, publicRsaKey);
            try
            {
                string decryptedText = Decrypt(encryptedText);

                return res.testText.Equals(decryptedText, StringComparison.CurrentCulture);
            }
            catch(FileEmptyException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
