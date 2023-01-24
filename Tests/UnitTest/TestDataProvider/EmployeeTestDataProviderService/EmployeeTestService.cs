using Application.Dtos;
using Application.Types;
using Domain.Entities;
using TestDataProvider.Interfaces;

namespace TestDataProvider.EmployeeTestDataProviderService
{
    public class EmployeeTestService : IEmployeeTestService
    {
        private readonly List<Employee> _employees;
        private readonly List<EmployeeDto> _employeesDto;
        public EmployeeTestService()
        {
            _employees = new List<Employee>
            {
                new Employee { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new Employee { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new Employee { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };

            _employeesDto = new List<EmployeeDto>
            {
                new EmployeeDto { Id=1, FirstName="Subash", LastName="Barik", Age=43, DepartmentId=1, DesignationId=1},
                new EmployeeDto { Id=2, FirstName="Nirupama", LastName="Pradhan", Age=37, DepartmentId=2, DesignationId=2},
                new EmployeeDto { Id=3, FirstName="Sunayana", LastName="Barik", Age=12, DepartmentId=3, DesignationId=3}
            };

        }
        public Employee GetEmployee(int id)
        {
            return _employees.FirstOrDefault(x => x.Id == id);
        }

        public EmployeeDto GetEmployeeDto(int id)
        {
            return _employeesDto.FirstOrDefault(x => x.Id == id);
        }

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public List<EmployeeDto> GetEmployeesDto()
        {
            return _employeesDto;
        }

        public Pagination<EmployeeDto> GetPagedData()
        {
            return new Pagination<EmployeeDto>(1, 10, 100, GetEmployeesDto());
        }
    }
}
