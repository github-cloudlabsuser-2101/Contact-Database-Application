using NUnit.Framework;
using leg = NUnit.Framework.Legacy;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Web.Mvc;
using System.Linq;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTests
    {
        private UserController _controller;

        [SetUp]
        public void Setup()
        {
            // Clear the userlist and initialize the controller before each test
            UserController.userlist.Clear();
            _controller = new UserController();
        }

        private void AddTestUsers()
        {
            UserController.userlist.Add(new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" });
            UserController.userlist.Add(new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" });
        }
        [Test]
        public void Details_WithValidId_ReturnsUser()
        {
            // Arrange
            AddTestUsers();
            int testUserId = 1;

            // Act
            var result = _controller.Details(testUserId) as ViewResult;

            // Assert
            leg.ClassicAssert.IsNotNull(result);
            var model = result.Model as User;
            leg.ClassicAssert.IsNotNull(model);
            leg.ClassicAssert.AreEqual(testUserId, model.Id);
        }

        [Test]
        public void Details_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            AddTestUsers();

            // Act
            var result = _controller.Details(99);

            // Assert
            leg.ClassicAssert.IsInstanceOf<HttpNotFoundResult>(result);
        }
        [Test]
        public void Edit_Get_WithValidId_ReturnsUser()
        {
            // Arrange
            AddTestUsers();
            int testUserId = 1;

            // Act
            var result = _controller.Edit(testUserId) as ViewResult;

            // Assert
            leg.ClassicAssert.IsNotNull(result);
            var model = result.Model as User;
            leg.ClassicAssert.IsNotNull(model);
            leg.ClassicAssert.AreEqual(testUserId, model.Id);
        }

        [Test]
        public void Edit_Post_UpdatesUser_AndRedirects()
        {
            // Arrange
            AddTestUsers();
            var updatedUser = new User { Id = 1, Name = "Updated Name", Email = "updated@example.com" };

            // Act
            var result = _controller.Edit(updatedUser.Id, updatedUser) as RedirectToRouteResult;

            // Assert
            leg.ClassicAssert.IsNotNull(result);
            leg.ClassicAssert.AreEqual("Index", result.RouteValues["action"]);
            var user = UserController.userlist.FirstOrDefault(u => u.Id == updatedUser.Id);
            leg.ClassicAssert.IsNotNull(user);
            leg.ClassicAssert.AreEqual(updatedUser.Name, user.Name);
        }
        [Test]
        public void Delete_Get_WithValidId_ReturnsUser()
        {
            // Arrange
            AddTestUsers();
            int testUserId = 1;

            // Act
            var result = _controller.Delete(testUserId) as ViewResult;

            // Assert
            leg.ClassicAssert.IsNotNull(result);
            var model = result.Model as User;
            leg.ClassicAssert.IsNotNull(model);
            leg.ClassicAssert.AreEqual(testUserId, model.Id);
        }

        [Test]
        public void Delete_Post_RemovesUser_AndRedirects()
        {
            // Arrange
            AddTestUsers();
            int testUserId = 1;

            // Act
            var result = _controller.Delete(testUserId, null) as RedirectToRouteResult; // Assuming DeleteConfirmed is the actual method name for POST

            // Assert
            leg.ClassicAssert.IsNotNull(result);
            leg.ClassicAssert.AreEqual("Index", result.RouteValues["action"]);
            var user = UserController.userlist.FirstOrDefault(u => u.Id == testUserId);
            leg.ClassicAssert.IsNull(user);
        }
    }
}
