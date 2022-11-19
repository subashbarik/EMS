using Application.Dtos;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public sealed record DeleteDepartmentCommand(DepartmentDto Department):IRequest<int>;
}