using System;

namespace Gama.Intranet.Management
{
    public class crypto
    {
        public static string Encrypt(string _stringToEncript)
        {
            try
            {
                byte[] encrypted = System.Text.Encoding.UTF8.GetBytes(_stringToEncript);
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception)
            {
                return _stringToEncript;
            }
        }

        public static string Decrypt(string _stringToDecrypt)
        {
            try
            {
                byte[] decrypted = Convert.FromBase64String(_stringToDecrypt);
                return System.Text.Encoding.Unicode.GetString(decrypted);
            }
            catch (Exception)
            {
                return _stringToDecrypt;
            }
        }
    }
}
