using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DataProvider
{
    public class Helper
    {
        public static class MD5
        {
            private static readonly string SECRET = "_DITAGIS";
            public static string Crypto(string chuoi)
            {
                //Hash the password
                MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
                byte[] hashedDataBytes;
                UTF8Encoding encoder = new UTF8Encoding();
                hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(chuoi));
                string str;
                str = BitConverter.ToString(hashedDataBytes).Replace("-", String.Empty).ToLower();
                return str;
            }
            public static string CryptoPassword(string password)
            {
                return Crypto(password + SECRET);
            }
        }
        public static string Query(string query)
        {
            var q = query.Replace("\r\n", "");
            q = q.Replace("\t", "");
            return q;
        }
    }
}
