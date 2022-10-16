using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmployeeService.Commands
{
    public record UpdateEmployeeCommand(EmployeeDto employee):IRequest<EmployeeDto>;
    
}
