using Application.Dtos;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public sealed record UpdateDepartmentCommand(DepartmentDto Department):IRequest<DepartmentDto>;
}