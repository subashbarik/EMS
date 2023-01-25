using Application.Dtos;
using PresentationTest.Helpers;
using PresentationTest.Interfaces;
using ReportGeneratorTests;


namespace PresentationTest.Tests.Account
{
    [Collection("API Extent Report Collection")]
    public class AccountControllerTests
    {
        private readonly ITestApiHelper _testApiHelper;
        private readonly ExtentReportTestFixture _extentReportTestFixture;
        public AccountControllerTests(ExtentReportTestFixture extentReportTestFixture)
        {
            _testApiHelper = new TestApiHelper();
            _extentReportTestFixture = extentReportTestFixture;
        }
        [Fact]
        public async void AccountController_Login_Valid_user()
        {
            try
            {
                //Arrange
                _extentReportTestFixture.CreateTest("Login with valid user");

                //TODO : read the loginDto details from a JSON File.
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
                if ((int)restResponse.StatusCode == 0)
                {
                    throw new Exception("Unable to fetch data from the API server");
                }
                var data = TestHandleContent.GetContent<UserDto>(restResponse);
                var statusCode = (int)restResponse.StatusCode;

                //Assert
                Assert.Equal(200, statusCode);
                _extentReportTestFixture.LogReport("Pass", "Login with valid user passed");
            }
            catch (Exception ex)
            {
                _extentReportTestFixture.LogReport("Error", ex.Message);
            }
            
        }
        [Fact]
        public async void AccountController_Login_Invalid_user()
        {
            try
            {
                //Arrange
                _extentReportTestFixture.CreateTest("Login with Invalid user");
                //TODO : read the loginDto details from a JSON File.
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
                if ((int)restResponse.StatusCode == 0)
                {
                    throw new Exception("Unable to fetch data from the API server");
                }
                var statusCode = (int)restResponse.StatusCode;

                //Assert
                Assert.Equal(401, statusCode);
                _extentReportTestFixture.LogReport("Pass", "Login with Invalid user passed");
            }
            catch (Exception ex)
            {
                _extentReportTestFixture.LogReport("Error", ex.Message);
            }
            
        }
    }
}
