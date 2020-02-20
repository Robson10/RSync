using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSync.Helpers
{
    internal static class Singleton
    {
        public static int? UserId { get; set; }
        
        public static RSAParameters RsaPublicKey { get; set; }
    }
}
