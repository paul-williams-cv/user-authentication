namespace UserAuthentication.Store.Database
{
    using System.Data.Entity;

    public sealed class AuthContext : DbContext
    {
        public AuthContext() : base("RegistrationDetails") { }

        public IDbSet<AuthenticationDetailsEntity> AuthenticationDetails { get; set; }
    }
}
