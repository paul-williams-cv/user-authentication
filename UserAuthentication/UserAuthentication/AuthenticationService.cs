namespace UserAuthentication
{
    using System;
    using UserAuthentication.Core;

    public sealed class AuthenticationService
    {
        // Instantiated through AuthenticationServiceFactory
        internal AuthenticationService(IAuthenticationDetailsStore store)
        {
            this.Store = store;
        }

        private IAuthenticationDetailsStore Store { get; }

        /// <summary>
        ///     Checks supplied credentials against registered credentials
        /// </summary>
        /// <param name="email">A registered email address</param>
        /// <param name="password">A registered password</param>
        /// <returns>true if credentials match</returns>
        public bool Login(string email, string password)
        {
            var registeredUser = this.Store.GetByEmail(email);

            if (registeredUser == null)
            {
                return false;
            }

            var hashedPassword = AuthenticationServiceHelper.Md5Hash(password);

            if (hashedPassword == registeredUser.Password)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Validates and persists registration details
        /// </summary>
        /// <param name="email">A valid email address</param>
        /// <param name="password">Password</param>
        public void Register(string email, string password)
        {
            if (!AuthenticationServiceHelper.IsEmailValid(email))
            {
                throw new ArgumentException("Email address is not valid");
            }

            if (this.Store.GetByEmail(email) != null)
            {
                throw new ArgumentException("This email address is already registered");
            }

            this.Store.Insert(
                new AuthenticationDetails
                {
                    Email = email,
                    Password = AuthenticationServiceHelper.Md5Hash(password)
                });
        }
    }
}
