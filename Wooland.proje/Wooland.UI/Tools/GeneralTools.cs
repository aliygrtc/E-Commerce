using System.Security.Cryptography;
using System.Text;

namespace Wooland.UI.Tools
{
    public class GeneralTools
    {
        public static string GetMD5(string _text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(_text));
                return BitConverter.ToString(hash).Replace("-", "");
            }
        }
    }
}
