using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TrainingTracker.Common.Utils
{
    public class CryptoUtils
    {
        public static byte[] CreatePasswordHashBytes(byte[] plainTextPassword, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainTextPassword.Length + salt.Length];

            for (int i = 0; i < plainTextPassword.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainTextPassword[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainTextPassword.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);   
        }

        public static string CreatePasswordHash(string plainTextPassword, string salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextPasswordBytes = FromBase64String(plainTextPassword);
            byte[] saltBytes = FromBase64String(salt);

            var hashedBytes = CreatePasswordHashBytes(plainTextPasswordBytes, saltBytes);

            return Convert.ToBase64String(hashedBytes);   
        }

        public static byte[] CreateSaltBytes(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return buff;
        }

        public static string CreateSalt(int size)
        {
            return Convert.ToBase64String(CreateSaltBytes(size));
        }

        public static byte[] FromBase64String(string data)
        {
            string s = data.Trim().Replace(" ", "+");
            if (s.Length % 4 > 0)
            {
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
            }
            return Convert.FromBase64String(s);
        }
    }
}
