using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace SFC_Tools.Classes
{
    public class SecretHelper
    {
        public byte[] Decrypt(string data, string password, string salt)
        {
            return Decrypt(Encoding.UTF8.GetBytes(data), password, salt);
        }

        public static byte[] Decrypt(byte[] data, string password, string salt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(salt);
            AesManaged managed = new AesManaged();
            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, buffer);
            managed.BlockSize = managed.LegalBlockSizes[0].MaxSize;
            managed.KeySize = managed.LegalKeySizes[0].MaxSize;
            managed.Key = bytes.GetBytes(managed.KeySize / 8);
            managed.IV = bytes.GetBytes(managed.BlockSize / 8);
            ICryptoTransform transform = managed.CreateDecryptor();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(data, 0, data.Length);
            stream2.Close();
            return stream.ToArray();
        }

        public static string DecryptFromBase64String(string data, string password, string slat)
        {
            byte[] bytes = Decrypt(Convert.FromBase64String(data), password, slat);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static string DecryptFromBase64String(string data)
        {
            byte[] bytes = PrivateSecretHelper.Decrypt(Convert.FromBase64String(data), "", "");
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] Encrypt(string data, string password, string salt)
        {
            return Encrypt(Encoding.UTF8.GetBytes(data), password, salt);
        }

        public static byte[] Encrypt(byte[] data, string password, string salt)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(salt);
            AesManaged managed = new AesManaged();
            Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, buffer);
            managed.BlockSize = managed.LegalBlockSizes[0].MaxSize;
            managed.KeySize = managed.LegalKeySizes[0].MaxSize;
            managed.Key = bytes.GetBytes(managed.KeySize / 8);
            managed.IV = bytes.GetBytes(managed.BlockSize / 8);
            ICryptoTransform transform = managed.CreateEncryptor();
            MemoryStream stream = new MemoryStream();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
            stream2.Write(data, 0, data.Length);
            stream2.Close();
            return stream.ToArray();
        }

        public static string EncryptToBase64String(string data, string password, string slat)
        {
            return Convert.ToBase64String(Encrypt(data, password, slat));
        }

        public static byte[] PrivateEncrypt(string data, string password, string salt, bool encryptedPasswordSalt = false)
        {
            string str = string.Empty;
            string str2 = string.Empty;
            if (!string.IsNullOrEmpty(str) && encryptedPasswordSalt) str = PrivateSecretHelper.DecryptFromBase64String(password, "", "");
            if (!string.IsNullOrEmpty(str2) && encryptedPasswordSalt) str2 = PrivateSecretHelper.DecryptFromBase64String(salt, "", "");
            return PrivateSecretHelper.Encrypt(data, str, str2);
        }
    }
}
