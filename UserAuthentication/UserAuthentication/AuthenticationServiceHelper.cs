namespace UserAuthentication
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    internal static class AuthenticationServiceHelper
    {
        internal static string Md5Hash(string text)
        {
            var md5 = MD5.Create();

            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            var stringBuilder = new StringBuilder();
            foreach (var t in result)
            {
                stringBuilder.Append(t.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        internal static bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(
                    email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase,
                    TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
