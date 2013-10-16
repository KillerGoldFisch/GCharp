using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace GSharp.Extensions.StringEx {
    public static class StringExtensions {
        public static byte[] ToByteArray(this string data) {
            return Encoding.UTF8.GetBytes(data);
        }
        /// <summary>
        /// Compares the string against a given pattern.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="wildcard">The wildcard, where "*" means any sequence of characters, and "?" means any single character.</param>
        /// <returns><c>true</c> if the string matches the given pattern; otherwise <c>false</c>.</returns>
        public static bool Like(this string str, string wildcard)
        {
            return new Regex(
                "^" + Regex.Escape(wildcard).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
                RegexOptions.IgnoreCase | RegexOptions.Singleline
            ).IsMatch(str);
        }

        public static string GetMD5Hash(this string TextToHash) {
            //Prüfen ob Daten übergeben wurden.
            if ((TextToHash == null) || (TextToHash.Length == 0)) {
                return string.Empty;
            }

            //MD5 Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
            //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            return System.BitConverter.ToString(result);
        }

        public static string Encrypt(this string str, string password = null) {
            return GSharp.Data.Crypto.EncDec.Encrypt(str, (password != null) ? password : GSharp.Data.Crypto.EncDec.GeneratePassword());
        }

        public static string Decrypt(this string str, string password = null) {
            return GSharp.Data.Crypto.EncDec.Decrypt(str, (password != null) ? password : GSharp.Data.Crypto.EncDec.GeneratePassword());
        }

        public static string Filter(this string str, char[] filter, bool whitelist) {
            IEnumerable<char> chars;

            if (whitelist)
                chars = str.Where(c => filter.Contains(c));
            else
                chars = str.Where(c => !filter.Contains(c));

            return new string(chars.ToArray());
        }

        #region Truncate
        /// <summary>
        /// Truncates the string to a specified length and replace the truncated to a ...
        /// </summary>
        /// <param name="maxLength">total length of characters to maintain before the truncate happens</param>
        /// <returns>truncated string</returns>
        public static string Truncate(this string text, int maxLength) {
            // replaces the truncated string to a ...
            const string suffix = "...";
            string truncatedString = text;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (text == null || text.Length <= maxLength) return truncatedString;

            truncatedString = text.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }
        #endregion

        #region RepeatToLen
        public static string RepeatToLen(this string text, int len) {
            string tmp = text;
            while (tmp.Length < len)
                tmp += text;
            return tmp.Substring(0, len);
        }
        #endregion

        #region Fill
        public enum TextAlighnment {
            Left,
            Right,
            Center
        }

        public static string Fill(this string text, int len, TextAlighnment alignment = TextAlighnment.Left) {
            if (text.Length > len)
                return text.Truncate(len);
            if (text.Length == len)
                return text;
            if (alignment == TextAlighnment.Left)
                return text + " ".RepeatToLen(len - text.Length);
            else if(alignment == TextAlighnment.Right)
                return " ".RepeatToLen(len - text.Length) + text;

            int spaceToFill = len - text.Length;
            int spaceToFillR = spaceToFill/ 2;
            int spaceToFillL = spaceToFill - spaceToFillR;

            return " ".RepeatToLen(spaceToFillL) + text + " ".RepeatToLen(spaceToFillR);
        }
        #endregion
    }
}
