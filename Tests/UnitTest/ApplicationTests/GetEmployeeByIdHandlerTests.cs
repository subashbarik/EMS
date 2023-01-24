using Domain.Interfaces;
using Domain.Entities;
using Application.EmployeeService.Queries;
using AutoMapper;
using Application.Dtos;
using TestDataProvider.Interfaces;
using TestDataProvider.EmployeeTestDataProviderService;

namespace ApplicationTests
{
    //[ExcludeFromCodeCoverage]
    public class GetEmployeeByIdHandlerTests
    {
        private readonly IEmployeeTestService _employeeTestService;
        public GetEmployeeByIdHandlerTests()
        {
            _employeeTestService = new EmployeeTestService();
        }

        [Fact]
        public async void GetEmployeeByIdHandler_Handle_With_Id_PresentInDB()
        {
            CancellationToken cancellationToken = new();
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                int id = 1;

                Employee employee = _employeeTestService.GetEmployee(id);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.Repository<Employee>().GetByIdAsync(id,cancellationToken))
                    .ReturnsAsync(employee);
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Employee, EmployeeDto>(employee))
                    .Returns(_employeeTestService.GetEmployeeDto(id));


                // Act
                var cls = mock.Create<GetEmployeeByIdHandler>();
                var queryClass = new GetEmployeeByIdQuery(id);
                var cancelToken = new CancellationToken();

                var expected = _employeeTestService.GetEmployeeDto(id);
                var actual = await cls.Handle(queryClass, cancelToken);

                // Assert
                Assert.True(expected != null);
                Assert.True(actual != null);

                Assert.Equal(expected.Id, actual.Id);
                Assert.Equal(expected.FirstName, actual.FirstName);
                Assert.Equal(expected.LastName, actual.LastName);
                Assert.Equal(expected.Age, actual.Age);
                Assert.Equal(expected.Basic, actual.Basic);
                Assert.Equal(expected.DepartmentId, actual.DepartmentId);
                Assert.Equal(expected.DesignationId, actual.DesignationId);

            }

        }

        [Fact]
        public async void GetEmployeeByIdHandler_Handle_With_Id_Not_PresentInDB()
        {
            CancellationToken cancellationToken = new();
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                int id = -1;

                Employee employee = _employeeTestService.GetEmployee(id);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.Repository<Employee>().GetByIdAsync(id,cancellationToken))
                    .ReturnsAsync(employee);
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Employee, EmployeeDto>(employee))
                    .Returns(_employeeTestService.GetEmployeeDto(id));


                // Act
                var cls = mock.Create<GetEmployeeByIdHandler>();
                var queryClass = new GetEmployeeByIdQuery(id);
                var cancelToken = new CancellationToken();

                var expected = _employeeTestService.GetEmployeeDto(id);
                var actual = await cls.Handle(queryClass, cancelToken);

                // Assert
                Assert.True(expected == null);
                Assert.True(actual == null);
            }

        }
       
    }
}