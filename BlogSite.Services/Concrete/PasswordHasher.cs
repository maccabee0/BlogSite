using System;
using System.Security.Cryptography;

namespace BlogSite.Services.Concrete
{
    public class PasswordHasher
    {
        public static byte[] CreatePasswordHash(string pwd)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] hashedPwd = Convert.FromBase64String(pwd);
            
            return algorithm.ComputeHash(hashedPwd);
        }

        public static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if(array1.Length != array2.Length)
            {
                return false;
            }
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
