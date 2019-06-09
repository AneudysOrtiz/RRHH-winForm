using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RRHHOrtiz.Helpers
{
    public static class PasswordHelper
    {
       public static string HashPassword(string originalPassword)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();

            byte[] inputBytes = new UnicodeEncoding().GetBytes(originalPassword);

            byte[] hash = sha.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);
        }
    }
}
