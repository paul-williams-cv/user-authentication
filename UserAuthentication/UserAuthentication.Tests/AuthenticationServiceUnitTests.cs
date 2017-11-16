namespace UserAuthentication.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using UserAuthentication.Core;

    [TestClass]
    public sealed class AuthenticationServiceUnitTests
    {
        private AuthenticationDetails RawDetails { get; set; }

        private AuthenticationDetails HashedDetails { get; set; }

        [TestInitialize]
        public void SetUp()
        {
            this.RawDetails = new AuthenticationDetails { Email = "a@b.com", Password = "let-me-in" };
            this.HashedDetails = new AuthenticationDetails
            {
                Email = "a@b.com",
                Password = AuthenticationServiceHelper.Md5Hash("let-me-in")
            };
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AuthenticationService_LoginWithCorrectCredentials_Succeeds()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            store.Setup(x => x.GetByEmail(this.RawDetails.Email)).Returns(this.HashedDetails);
            var service = new AuthenticationService(store.Object);

            // Act
            var loggedIn = service.Login(this.RawDetails.Email, this.RawDetails.Password);

            // Assert
            Assert.IsTrue(loggedIn);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AuthenticationService_LoginWithUnknownEmail_Fails()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            store.Setup(x => x.GetByEmail(this.RawDetails.Email)).Returns(this.HashedDetails);
            store.Setup(x => x.GetByEmail("unknown@b.com")).Returns((AuthenticationDetails)null);
            var service = new AuthenticationService(store.Object);

            // Act
            var loggedIn = service.Login("unknown@b.com", this.RawDetails.Password);

            // Assert
            Assert.IsFalse(loggedIn);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AuthenticationService_LoginWithWrongPassword_Fails()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            store.Setup(x => x.GetByEmail(this.RawDetails.Email)).Returns(this.HashedDetails);
            var service = new AuthenticationService(store.Object);

            // Act
            var loggedIn = service.Login(this.RawDetails.Email, "wrong-password");

            // Assert
            Assert.IsFalse(loggedIn);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void AuthenticationService_Register_Inserts()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            store.Setup(x => x.GetByEmail(this.RawDetails.Email)).Returns((AuthenticationDetails)null);
            store.Setup(x => x.Insert(It.IsAny<AuthenticationDetails>())).Verifiable();
            var service = new AuthenticationService(store.Object);

            // Act
            service.Register(this.RawDetails.Email, this.RawDetails.Password);

            // Assert
            store.Verify(x => x.Insert(It.IsAny<AuthenticationDetails>()));
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticationService_RegisterWithExistingEmail_Fails()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            store.Setup(x => x.GetByEmail(this.RawDetails.Email)).Returns(this.HashedDetails);
            var service = new AuthenticationService(store.Object);

            // Act
            service.Register(this.RawDetails.Email, this.RawDetails.Password);

            // Assert
            // Exception expected
        }

        [TestMethod]
        [TestCategory("Unit")]
        [ExpectedException(typeof(ArgumentException))]
        public void AuthenticationService_RegisterWithInvalidEmail_Fails()
        {
            // Arrange
            var store = new Mock<IAuthenticationDetailsStore>(MockBehavior.Strict);
            var service = new AuthenticationService(store.Object);

            // Act
            service.Register("invalidemail", this.RawDetails.Password);

            // Assert
            // Exception expected
        }
    }
}
