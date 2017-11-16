namespace UserAuthentication.Store.Database
{
    using System;

    public class AuthenticationDetailsEntity
    {
        public virtual Guid Id { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }
    }
}
