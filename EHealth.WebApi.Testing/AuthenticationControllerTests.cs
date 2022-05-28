using EHealth.Services;
using EHealth.WebApi.Controllers;
using EHealth.WebApi.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System.Threading.Tasks;

namespace EHealth.WebApi.Testing
{
    class AuthenticationControllerTests
    {
        private AuthenticationController stubbedController;

        [SetUp]
        public void Setup()
        {
            stubbedController = new(new StubIdentityService());
        }

        [Test]
        public async Task Login_StubbedValues_ReturnsCode200()
        {
            LoginViewModel model = new() 
            {
                UserName = "",
                Password = "",
            };
            IActionResult result;

            result = await stubbedController.Login(model);

            Assert.That(result, Is.InstanceOf<OkObjectResult>());
        }

        [Test]
        public async Task Register_StubbedValues_ReturnsCode200()
        {
            RegisterViewModel model = new()
            {
                UserName = "",
                FullName = "",
                Password = "",
            };
            IActionResult result;

            result = await stubbedController.Register(model);

            Assert.That(result, Is.InstanceOf<OkResult>());
        }
    }
}
