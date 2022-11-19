using Domain.Interfaces;
using Domain.Entities;
using Application.EmployeeService.Queries;
using AutoMapper;
using Application.Dtos;



namespace ApplicationTests
{
    public class GetEmployeeByIdHandlerTests
    {

        [Fact]
        public async void GetEmployeeByIdHandler_Handle_With_Id_PresentInDB()
        {
            CancellationToken cancellationToken = new();
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                int id = 1;

                Employee employee = GetEmployee(id);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.Repository<Employee>().GetByIdAsync(id,cancellationToken))
                    .ReturnsAsync(employee);
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Employee, EmployeeDto>(employee))
                    .Returns(GetEmployeeDto(id));


                // Act
                var cls = mock.Create<GetEmployeeByIdHandler>();
                var queryClass = new GetEmployeeByIdQuery(id);
                var cancelToken = new CancellationToken();

                var expected = GetEmployeeDto(id);
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

                Employee employee = GetEmployee(id);

                mock.Mock<IUnitOfWork>()
                    .Setup(x => x.Repository<Employee>().GetByIdAsync(id,cancellationToken))
                    .ReturnsAsync(employee);
                mock.Mock<IMapper>()
                    .Setup(x => x.Map<Employee, EmployeeDto>(employee))
                    .Returns(GetEmployeeDto(id));


                // Act
                var cls = mock.Create<GetEmployeeByIdHandler>();
                var queryClass = new GetEmployeeByIdQuery(id);
                var cancelToken = new CancellationToken();

                var expected = GetEmployeeDto(id);
                var actual = await cls.Handle(queryClass, cancelToken);

                // Assert
                Assert.True(expected == null);
                Assert.True(actual == null);
            }

        }

        public Employee GetEmployee(int id)
        {
            List<Employee> employees = new()
            {
                new Employee { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new Employee { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new Employee { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };
            
            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }

        
        public EmployeeDto GetEmployeeDto(int id)
        {
            List<EmployeeDto> employees = new()
            {
                new EmployeeDto { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new EmployeeDto { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new EmployeeDto { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };
            var employee = employees.FirstOrDefault(e => e.Id == id);
            return employee;
        }
       
    }
}