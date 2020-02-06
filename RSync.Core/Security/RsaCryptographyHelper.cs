using RSync.AppResources.Configuration;
using RSync.Core.Converters;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RSync.Core.Security
{
    /// <summary>
    /// Used to encrypt/decrypt text data.
    /// </summary>
    public static class RsaCryptographyHelper
    {
        /// <summary>
        /// RSA public key.
        /// </summary>
        private static RSAParameters? _rsaPublicKey = null;

        /// <summary>
        /// RSA private key
        /// </summary>
        private static RSAParameters? _rsaPrivateKey = null;

        /// <summary>
        /// RSA byte count.
        /// </summary>
        private const int RsaKeySize = 4096;

        /// <summary>
        /// Property returned RSA public key
        /// </summary>
        private static RSAParameters RsaPublicKey
        {
            get
            {
                if (_rsaPublicKey == null)
                {
                    SetRsaKeys();
                }
                return (RSAParameters)_rsaPublicKey;
            }
        }

        /// <summary>
        /// Property returned RSA private key.
        /// </summary>
        private static RSAParameters RsaPrivateKey
        {
            get
            {
                if (_rsaPrivateKey == null)
                {
                    SetRsaKeys();
                }
                return (RSAParameters)_rsaPrivateKey;
            }
        }

        /// <summary>
        /// Read RSA public and private key from file or create new even if one of keys not exist.
        /// </summary>
        private static void SetRsaKeys()
        {
            string rsaPrivateKeyJson = File.ReadAllText(AppPaths.RsaPrivateKeyFilePath);
            string rsaPublicKeyJson = File.ReadAllText(AppPaths.RsaPublicKeyFilePath);

            if (string.IsNullOrEmpty(rsaPrivateKeyJson) || string.IsNullOrEmpty(rsaPublicKeyJson))
            {
                using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
                {
                    _rsaPrivateKey = csp.ExportParameters(true);
                    _rsaPublicKey = csp.ExportParameters(false);
                    csp.Dispose();
                }

                rsaPrivateKeyJson = _rsaPrivateKey.Serialize();
                rsaPublicKeyJson = _rsaPublicKey.Serialize();

                File.WriteAllText(AppPaths.RsaPrivateKeyFilePath, rsaPrivateKeyJson);
                File.WriteAllText(AppPaths.RsaPublicKeyFilePath, rsaPublicKeyJson);
            }
            else
            {
                _rsaPrivateKey = ObjectToJson.Deserialize<RSAParameters>(rsaPrivateKeyJson);
                _rsaPublicKey = ObjectToJson.Deserialize<RSAParameters>(rsaPublicKeyJson);
            }
        }

        /// <summary>
        /// Encrypt plain text.
        /// </summary>
        /// <param name="plainText">Plain text</param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            string cipherText = string.Empty;
            byte[] bytesCypherText;

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
            {
                csp.ImportParameters(RsaPublicKey);
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
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
            {
                return string.Empty;
            }
            string plainTextData = string.Empty;
            byte[] bytesPlainTextData;
            byte[] bytesCypherText = Convert.FromBase64String(cipherText);

            using (RSACryptoServiceProvider csp = new RSACryptoServiceProvider(RsaKeySize))
            {
                csp.ImportParameters(RsaPrivateKey);
                //decrypt and strip pkcs#1.5 padding
                bytesPlainTextData = csp.Decrypt(bytesCypherText, false);
            }

            if (bytesPlainTextData != null && bytesPlainTextData.Length > 0)
            {
                plainTextData = Encoding.Unicode.GetString(bytesPlainTextData);
            }

            return plainTextData;
        }
    }
}
