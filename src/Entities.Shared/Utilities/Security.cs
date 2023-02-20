using System;
using System.Text;

namespace Entities.Shared.Utilities
{
    public static class Security
    {
        /// Encripta una cadena
        public static string Encrypt(string value)
        {
            byte[] encrypted = Encoding.Unicode.GetBytes(value);
            return Convert.ToBase64String(encrypted); ;
        }

        /// Esta función desencripta la cadena que le envíamos en el parámentro de entrada.
        public static string Decrypt(string value)
        {
            byte[] decrypted = Convert.FromBase64String(value);
            return Encoding.Unicode.GetString(decrypted);
        }
    }
}
