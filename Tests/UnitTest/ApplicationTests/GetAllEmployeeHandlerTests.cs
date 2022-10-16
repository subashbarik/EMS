using Application.Dtos;
using Application.EmployeeService.Queries;
using Application.Types;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using Domain.Types;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class GetAllEmployeeHandlerTests
    {

        [Fact]
        public async void GetEmployeeByIdHandler_Handle_With_Id_PresentInDB()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                int employeeCount = 100;
                List<Employee> employees = new List<Employee>();
                employees = GetEmployees();

                var employee = GetEmployee();

                IReadOnlyList<Application.Dtos.EmployeeDto> employeesDtos;
                employeesDtos = GetEmployeesDto();

                mock.Mock<IGenericDapperRepository>()
                    .Setup(x => x.CountData("usp_GetEmployeeCount"))
                    .ReturnsAsync(employeeCount);

                mock.Mock<IGenericDapperRepository>()
                    .Setup(x => x.LoadData<Employee, object>("usp_GetAllEmployees", It.IsAny<object>()))
                    .ReturnsAsync(employees);

                mock.Mock<IMapper>()
                    .Setup(x => x.Map<IReadOnlyList<Employee>, IReadOnlyList<EmployeeDto>>(employees))
                    .Returns(employeesDtos);

                mock.Mock<ILogger<GetAllEmployeeHandler>>();
                    
                    

                // Act
                var cls = mock.Create<GetAllEmployeeHandler>();
                EmployeeSpecParams employeeParams = new EmployeeSpecParams();
                employeeParams.PageIndex = 1;
                employeeParams.PageSize = 10;
                
                var queryClass = new GetAllEmployeeQuery(employeeParams);
                var cancelToken = new CancellationToken();

                var expected = GetPagedData();
                var actual = await cls.Handle(queryClass, cancelToken);

                // Assert
                Assert.True(expected != null);
                Assert.True(actual != null);

                Assert.Equal(expected.Count, actual.Count);

                Assert.Equal(expected.Data.Count, actual.Data.Count);
                //Assert.Equal(expected.Id, actual.Id);
                //Assert.Equal(expected.FirstName, actual.FirstName);
                //Assert.Equal(expected.LastName, actual.LastName);
                //Assert.Equal(expected.Age, actual.Age);
                //Assert.Equal(expected.Salary, actual.Salary);
                //Assert.Equal(expected.DepartmentId, actual.DepartmentId);
                //Assert.Equal(expected.DesignationId, actual.DesignationId);

            }

        }
        public Domain.Entities.Employee GetEmployee()
        {
            return new Employee { Id = 1, FirstName = "Subash", LastName = "Barik", Age = 43, DepartmentId = 1, DesignationId = 1 };
        }
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new()
            {
                new Employee { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new Employee { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new Employee { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };
            return employees;
        }


        public IReadOnlyList<Application.Dtos.EmployeeDto> GetEmployeesDto()
        {
            List<Application.Dtos.EmployeeDto> employees = new()
            {
                new Application.Dtos.EmployeeDto { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new Application.Dtos.EmployeeDto { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new Application.Dtos.EmployeeDto { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };
            
            return employees;
        }
        public Pagination<Application.Dtos.EmployeeDto> GetPagedData()
        {
            return new Pagination<Application.Dtos.EmployeeDto>(1, 10, 100, GetEmployeesDto());
        }
    }
    
}
