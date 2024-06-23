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
        private static byte[] _key = Encoding.UTF8.GetBytes("12457898"); // 8 bytes for DES
        private static byte[] _iv = Encoding.UTF8.GetBytes("12457898");    // 8 bytes for DES

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

        //MemoryStream memoryStream: This is a .NET class used to store binary data in memory. In this context, it's used to collect the output of the DES encryption operation. MemoryStream provides a stream interface to an in-memory block of data, allowing you to read from or write to it like any other stream.
        //CryptoStream: This class in .NET is used to provide cryptographic transformations on data. It works in conjunction with other stream classes (like MemoryStream in this case) to perform encryption or decryption on data as it's being read from or written to the stream
        //cryptoStream.FlushFinalBlock(): This method call is used to ensure that any remaining data in the CryptoStream is processed and written to the underlying stream (memoryStream in this case).

        /*bytes: This is the array of bytes that you want to write to the CryptoStream. In this case, it contains the UTF-8 encoded bytes of the plaintext string (plainText).
            0: This is the starting index in the bytes array from which the bytes will be read for writing to the CryptoStream. In C#, array indices are zero-based, so 0 indicates that the bytes will be read starting from the beginning of the array.
              bytes.Length: This is the number of bytes to be written to the CryptoStream. In this case, it's the length of the bytes array, which corresponds to the length of the UTF-8 encoded plaintext string.*/
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
