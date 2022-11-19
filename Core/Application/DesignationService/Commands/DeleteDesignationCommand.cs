using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DesignationService.Commands
{   
    public sealed record DeleteDesignationCommand(DesignationDto Designation) : IRequest<int>;
}
