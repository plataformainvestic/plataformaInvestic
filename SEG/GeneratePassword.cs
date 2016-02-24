using System.Security.Cryptography;
using System.Text;

namespace SEG
{
    public class GeneratePassword
    {
        public static string GetUniqueKey(int maxSize)
        {
            var chars = new char[62];
            chars =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[1];
            var crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
            var result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b%(chars.Length)]);
            }
            return result.ToString();
        }
    }
}