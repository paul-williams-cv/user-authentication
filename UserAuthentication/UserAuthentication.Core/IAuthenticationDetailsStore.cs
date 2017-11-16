namespace UserAuthentication.Core
{
    public interface IAuthenticationDetailsStore
    {
        AuthenticationDetails GetByEmail(string email);

        void Insert(AuthenticationDetails details);
    }
}
