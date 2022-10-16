using Application.Models.Page;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmployeeService.Queries
{
    public record GetEmployeeFormPageQuery: IRequest<EmployeeFormPageModel>;
    
}
