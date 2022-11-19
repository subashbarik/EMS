using Application.Dtos;
using MediatR;

namespace Application.DesignationService.Commands
{   
    public sealed record InsertDesignationCommand(DesignationDto Designation) : IRequest<DesignationDto>;
}
