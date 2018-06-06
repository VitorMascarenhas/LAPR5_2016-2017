using Microsoft.AspNet.Identity;
using Moq;
using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB.Tests.Mocks.Identity
{
    public class UserManagerMock
    {
        public static UserManager<User> Create()
        {
            // create our mocked user
            var user = new User { UserName = "admin", Email = "admin@wvm147.com" };

            // mock the application user manager with mocked user store
            var mockedUserStore = new Mock<IUserStore<User>>();
            var applicationUserManager = new Mock<UserManager<User>>(mockedUserStore.Object);

            // mock the application user manager to always return our user object with any username and password
            applicationUserManager.Setup(x => x.FindAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(user));

            // mock the application user manager create identity in order to generate valid access token when requested
            applicationUserManager.Setup(x => x.CreateIdentityAsync(It.IsAny<User>(), It.IsAny<string>()))
                .Returns<User, string>(
                    (u, password) =>
                        Task.FromResult(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserName) },
                            DefaultAuthenticationTypes.ApplicationCookie)));

            applicationUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).Returns(Task.FromResult(user));
            

            return applicationUserManager.Object;
        }
    }
}

