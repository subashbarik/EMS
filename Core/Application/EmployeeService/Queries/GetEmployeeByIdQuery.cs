using Application.Dtos;
using MediatR;

namespace Application.EmployeeService.Queries
{
    public record GetEmployeeByIdQuery(int Id) :IRequest<EmployeeDto>;
   
}
