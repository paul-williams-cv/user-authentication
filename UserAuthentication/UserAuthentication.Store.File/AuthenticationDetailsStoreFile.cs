namespace UserAuthentication.Store.File
{
    using System.IO;
    using System.Text;
    using UserAuthentication.Core;

    public sealed class AuthenticationDetailsStoreFile : IAuthenticationDetailsStore
    {
        public AuthenticationDetailsStoreFile(string path)
        {
            this.StorePath = path;
        }

        private string StorePath { get; }

        public AuthenticationDetails GetByEmail(string email)
        {
            try
            {
                var text = File.ReadAllText(this.StorePath + this.FileNameFromString(email));
                var details = text.Split(',');
                if (details[0].Equals(email))
                {
                    return new AuthenticationDetails { Email = details[0], Password = details[1] };
                }
            }
            catch (FileNotFoundException)
            {
                return null;
            }

            return null;
        }

        public void Insert(AuthenticationDetails details)
        {
            File.WriteAllText(this.StorePath + details.Email, details.Email + ',' + details.Password);
        }

        public string FileNameFromString(string source)
        {
            var fileName = new StringBuilder(source);
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }
            return fileName.ToString();
        }
    }
}
