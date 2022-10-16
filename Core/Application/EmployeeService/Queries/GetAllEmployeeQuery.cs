using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using MediatR;

namespace Application.EmployeeService.Queries
{
    public record GetAllEmployeeQuery(EmployeeSpecParams employeeParams) :IRequest<Pagination<EmployeeDto>>;
    
}
