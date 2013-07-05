#region credits
// ***********************************************************************
// Assembly	: TaskForceManager.Common
// Author	: Rod Johnson
// Created	: 03-23-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-28-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CoolChat.Common.Crypto
{
    #region

    

    #endregion

    public static class CryptoHelper
    {
        internal const char PasswordHashingIterationCountSeparator = '.';
        internal static Func<int> GetCurrentYear = () => DateTime.Now.Year;

        public static string Hash(string value)
        {
            return Crypto.Hash(value);
        }

        public static string GenerateSalt()
        {
            return Crypto.GenerateSalt();
        }

        public static string HashPassword(string password, int count)
        {
            if (count <= 0)
            {
                count = GetIterationsFromYear(GetCurrentYear());
            }
            var result = Crypto.HashPassword(password, count);
            return EncodeIterations(count) + PasswordHashingIterationCountSeparator + result;
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword.Contains(PasswordHashingIterationCountSeparator))
            {
                var parts = hashedPassword.Split(PasswordHashingIterationCountSeparator);
                if (parts.Length != 2) return false;

                int count = DecodeIterations(parts[0]);
                if (count <= 0) return false;

                hashedPassword = parts[1];
                
                return Crypto.VerifyHashedPassword(hashedPassword, password, count);
            }
            else
            {
                return Crypto.VerifyHashedPassword(hashedPassword, password);
            }
        }

        public static string EncodeIterations(int count)
        {
            return count.ToString("X");
        }

        public static int DecodeIterations(string prefix)
        {
            int val;
            if (Int32.TryParse(prefix, System.Globalization.NumberStyles.HexNumber, null, out val))
            {
                return val;
            }
            return -1;
        }

        // from OWASP : https://www.owasp.org/index.php/Password_Storage_Cheat_Sheet
        const int StartYear = 2000;
        const int StartCount = 1000;
        internal static int GetIterationsFromYear(int year)
        {
            if (year > StartYear)
            {
                var diff = (year - StartYear) / 2;
                var mul = (int)Math.Pow(2, diff);
                int count = StartCount * mul;
                // if we go negative, then we wrapped (expected in year ~2044). 
                // Int32.Max is best we can do at this point
                if (count < 0) count = Int32.MaxValue;
                return count;
            }
            return StartCount;
        }

        public static IEnumerable<string> ParseHashtag(string message)
        {
            var hashTag = new Regex(@"#\w+");

            while(hashTag.Match(message).Success)
            {
                string value = hashTag.Match(message).Groups[0].Value;
                message = message.Replace(value, "");
                yield return value;
            }
        }
    }
}
