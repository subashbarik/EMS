using Application.Dtos;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public sealed record InsertDepartmentCommand(DepartmentDto department): IRequest<DepartmentDto>;
}