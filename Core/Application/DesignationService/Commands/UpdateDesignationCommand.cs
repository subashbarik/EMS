using Application.Dtos;
using MediatR;

namespace Application.DesignationService.Commands
{   
    public sealed record UpdateDesignationCommand(DesignationDto Designation) : IRequest<DesignationDto>;
}
