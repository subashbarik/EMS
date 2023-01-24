using Application.Dtos;
using PresentationTest.Helpers;
using PresentationTest.Interfaces;
using RestSharp;

namespace PresentationTest.Tests.Account
{
    public class AccountControllerTests
    {
        private ITestApiHelper _testApiHelper;
        public AccountControllerTests()
        {
            _testApiHelper = new TestApiHelper();
        }
        [Fact]
        public async void AccountController_Login_Valid_user()
        {
            //Arrange
            LoginDto payLoad = new LoginDto()
            {
                Email = "subash.barik@gmail.com",
                Password = "Pa$$w0rd",
                RememberMe = false
            };
            _testApiHelper.SetUrl("http://localhost:5293/", "api/account/login");
            var restRequest = _testApiHelper.CreatePostRequest<LoginDto>(payLoad);
            

            // Act
            var restResponse = await _testApiHelper.GetResponseAsync();
            var data = TestHandleContent.GetContent<UserDto>(restResponse);
            var statusCode = (int) restResponse.StatusCode;

            //Assert
            Assert.Equal(200,statusCode);
        }
        [Fact]
        public async void AccountController_Login_Invalid_user()
        {
            //Arrange
            LoginDto payLoad = new LoginDto()
            {
                Email = "subash1111.barik1@gmail.com",
                Password = "Pa$$w0rd",
                RememberMe = false
            };
            _testApiHelper.SetUrl("http://localhost:5293/", "api/account/login");
            var restRequest = _testApiHelper.CreatePostRequest<LoginDto>(payLoad);

            // Act
            var restResponse = await _testApiHelper.GetResponseAsync();
            var statusCode = (int)restResponse.StatusCode;

            //Assert
            Assert.Equal(401, statusCode);
        }
    }
}
