using System;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace LaCasita
{
    static partial class Program
    {
        [System.Runtime.InteropServices.DllImport("wininet.dll", SetLastError = true)]
        private static extern bool InternetGetCookieEx(string url, string cookieName, StringBuilder cookieData, ref int size, int flags, IntPtr pReserved);
        [DebuggerStepThrough]
        public static string GetCookie(string url)
        {
            var size = 512;
            var stringBuilder = new StringBuilder(size);
            if (InternetGetCookieEx(url, null, stringBuilder, ref size, 0x00002000, IntPtr.Zero)) return stringBuilder.ToString();
            if (size < 0) return null;
            stringBuilder = new StringBuilder(size);
            return !InternetGetCookieEx(url, null, stringBuilder, ref size, 0x00002000, IntPtr.Zero) ? null : stringBuilder.ToString();
        }

        public static String RandomString()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetNonZeroBytes(data);
            data = new byte[8];
            rngCryptoServiceProvider.GetNonZeroBytes(data);
            var stringBuilder = new StringBuilder(8);
            foreach (var b in data)
            { stringBuilder.Append(chars[b % (chars.Length - 1)]); }
            return stringBuilder.ToString();
        }


    }
}
