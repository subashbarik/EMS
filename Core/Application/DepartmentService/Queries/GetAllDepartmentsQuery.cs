using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using MediatR;

namespace Application.DepartmentService.Queries
{
    public record GetAllDepartmentsQuery(DepartmentSpecParams departmentParams):IRequest<Pagination<DepartmentDto>>;
    
}