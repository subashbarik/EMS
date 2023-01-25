using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using PresentationTest.Helpers;
using PresentationTest.Interfaces;
using ReportGeneratorTests;
using System.Reflection;

namespace PresentationTest.Tests.Department
{
    [Collection("API Extent Report Collection")]
    public class DepartmentControllerTests
    {
        private readonly ExtentReportTestFixture _extentReportTestFixture;
        private readonly ITestApiHelper _testApiHelper;

        public DepartmentControllerTests(ExtentReportTestFixture extentReportTestFixture)
        {
            _testApiHelper = new TestApiHelper();
            _extentReportTestFixture = extentReportTestFixture;
        }
        [Fact]
        public async void DepartmentsController_GetDepartments_With_Admin_login()
        {
            try
            {
                //Arrange
                //Login with admin user to get the bearer token first
                _extentReportTestFixture.CreateTest("Get Departments with Admin User");
                //TODO : read the loginDto details from a JSON File.
                LoginDto payLoad = new LoginDto()
                {
                    Email = "subash.barik@gmail.com",
                    Password = "Pa$$w0rd",
                    RememberMe = false
                };
                _testApiHelper.SetUrl("http://localhost:5293/", "api/account/login");
                var restRequest = _testApiHelper.CreatePostRequest<LoginDto>(payLoad);

                var restResponse = await _testApiHelper.GetResponseAsync();
                if ((int)restResponse.StatusCode == 0)
                {
                    throw new Exception("Unable to fetch data from the API server");
                }
                var data = TestHandleContent.GetContent<UserDto>(restResponse);

                DepartmentSpecParams departmentSpecParam = new DepartmentSpecParams();
                _testApiHelper.SetUrl("http://localhost:5293/", "api/departments");
                restRequest = _testApiHelper.CreateGetRequest<DepartmentSpecParams>(departmentSpecParam, data.Token);

                //Act
                restResponse = await _testApiHelper.GetResponseAsync();
                var departments = TestHandleContent.GetContent<Pagination<DepartmentDto>>(restResponse);
                var statusCode = (int)restResponse.StatusCode;

                //Assert
                Assert.Equal(200, statusCode);
                _extentReportTestFixture.LogReport("Pass", "Get Departments with Admin User");
            }
            catch (Exception ex)
            {
                _extentReportTestFixture.LogReport("Error", ex.Message);
            }
            
        }
    }
}
