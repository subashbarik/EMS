using Application.Dtos;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public sealed record DeleteDepartmentCommand(DepartmentDto department):IRequest<int>;
}