using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helper
{
    public class CryptionHelper
    {
        private static byte[] _key = Encoding.UTF8.GetBytes("YourSecretKey"); // 8 bytes for DES
        private static byte[] _iv = Encoding.UTF8.GetBytes("YourIVValue");    // 8 bytes for DES

        public static string EncryptString(string plainText)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write))
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                        cryptoStream.Write(bytes, 0, bytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string DecryptString(string cipherText)
        {
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
