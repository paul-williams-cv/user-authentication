namespace UserAuthentication
{
    using UserAuthentication.Store.File;
#if NET461
    using UserAuthentication.Store.Database;
#endif

    public class AuthenticationServiceFactory
    {
        // To allow the two different store types to be used depending on TFM. Both available to 4.6.1. 
        // Only file available to standard 1.3
        public static AuthenticationService GetAuthenticationServiceFile(string path = @"C:\StoreFolder\")
        {
            var fileStore = new AuthenticationDetailsStoreFile(path);

            return new AuthenticationService(fileStore);
        }

#if NET461
        public static AuthenticationService GetAuthenticationServiceDatabase()
        {
            var dbStore = new AuthenticationDetailsStoreDatabase();
            dbStore.Initialize();

            return new AuthenticationService(dbStore);
        }
#endif
    }
}
