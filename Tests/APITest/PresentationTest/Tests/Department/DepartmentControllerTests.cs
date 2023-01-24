using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using PresentationTest.Helpers;
using PresentationTest.Interfaces;

namespace PresentationTest.Tests.Department
{
    public class DepartmentControllerTests
    {
        private ITestApiHelper _testApiHelper;

        public DepartmentControllerTests()
        {
            _testApiHelper = new TestApiHelper();
        }
        [Fact]
        public async void DepartmentsController_GetDepartments_With_Admin_login()
        {
            //Arrange
            //Login with admin user to get the bearer token first

            LoginDto payLoad = new LoginDto()
            {
                Email = "subash.barik@gmail.com",
                Password = "Pa$$w0rd",
                RememberMe = false
            };
            _testApiHelper.SetUrl("http://localhost:5293/", "api/account/login");
            var restRequest = _testApiHelper.CreatePostRequest<LoginDto>(payLoad);

            var restResponse = await _testApiHelper.GetResponseAsync();
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
        }
    }
}
