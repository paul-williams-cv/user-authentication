namespace UserAuthentication.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class AuthenticationServiceAcceptanceTests
    {
        [TestMethod]
        [TestCategory("Acceptance")]
        public void AuthenticationService_File_CanRegisterAndLogin()
        {
            // Arrange
            var service = AuthenticationServiceFactory.GetAuthenticationServiceFile();

            // Act
            try
            {
                service.Register("name@email.com", "mypassword");
            }
            catch (ArgumentException)
            {
            }

            var loggedIn = service.Login("name@email.com", "mypassword");

            // Assert
            Assert.IsTrue(loggedIn);
        }

        [TestMethod]
        [TestCategory("Acceptance")]
        public void AuthenticationService_Database_CanRegisterAndLogin()
        {
            // Arrange
            var service = AuthenticationServiceFactory.GetAuthenticationServiceDatabase();

            // Act
            try
            {
                service.Register("name@email.com", "mypassword");
            }
            catch (ArgumentException)
            {
            }

            var loggedIn = service.Login("name@email.com", "mypassword");

            // Assert
            Assert.IsTrue(loggedIn);
        }
    }
}
