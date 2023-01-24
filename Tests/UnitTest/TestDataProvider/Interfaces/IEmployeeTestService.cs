using Application.Dtos;
using Application.Types;
using Domain.Entities;

namespace TestDataProvider.Interfaces
{
    public interface IEmployeeTestService
    {
        Employee GetEmployee(int id);
        EmployeeDto GetEmployeeDto(int id);
        List<Employee> GetEmployees();
        List<EmployeeDto> GetEmployeesDto();
        Pagination<EmployeeDto> GetPagedData();
    }
}
