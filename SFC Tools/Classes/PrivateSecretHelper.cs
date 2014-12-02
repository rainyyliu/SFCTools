using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Classes
{
    internal class PrivateSecretHelper
    {
        public static byte[] Decrypt(byte[] data, string password = "", string slat = "")
        {
            string passwordSlat = GetPasswordSlat(password, true);
            string salt = GetPasswordSlat(slat, false);
            return SecretHelper.Decrypt(data, passwordSlat, salt);
        }

        public static byte[] Decrypt(string data, string password = "", string slat = "")
        {
            return Decrypt(Encoding.UTF8.GetBytes(data), password, slat);
        }

        public static string DecryptFromBase64String(string data, string password = "", string slat = "")
        {
            byte[] bytes = Decrypt(Convert.FromBase64String(data), password, slat);
            return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
        }

        public static byte[] Encrypt(string data, string password = "", string slat = "")
        {
            return Encrypt(Encoding.UTF8.GetBytes(data), password, slat);
        }

        public static byte[] Encrypt(byte[] data, string password = "", string slat = "")
        {
            string passwordSlat = GetPasswordSlat(password, true);
            string salt = GetPasswordSlat(slat, false);
            return SecretHelper.Encrypt(data, passwordSlat, salt);
        }

        public static string EncryptToBase64String(string data, string password = "", string slat = "")
        {
            return Convert.ToBase64String(Encrypt(data, password, slat));
        }

        private static string GetPasswordSlat(string format, bool password)
        {
            long ticks = DateTime.Now.Ticks;
            string str = "A+ Framework{0}{1}{2}{3}{4}";
            if (DateTime.Now.Ticks - ticks > 0x12a05f200L) throw new Exception();
            if (string.IsNullOrEmpty(format)) format = string.Empty;
            return (password ? string.Format(str, new object[] { "China", "Wales Wang", "1973.09.09", "Man", format }) : string.Format(str, new object[] { "中华人民共和国", "王智", "一九七三年九月九日", "男", format }));
        }
    }
}
