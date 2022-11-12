﻿using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmployeeService.Commands
{
    public sealed record DeleteEmployeeCommand(EmployeeDto employee):IRequest<int>;
   
}
