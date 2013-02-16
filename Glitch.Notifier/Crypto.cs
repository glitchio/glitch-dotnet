using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Glitch.Notifier
{
    internal static class Crypto
    {
        public static string Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                return Convert.ToBase64String(md5.ComputeHash(Encoding.UTF8.GetBytes(input)));
            }
        }
    }
}
