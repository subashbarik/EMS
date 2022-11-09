using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Types;
using Application.Dtos;
using Domain.Specifications;
using Application.DepartmentService.Queries;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    public class DepartmentsController:BaseApiController
    {
        private readonly IMediator _mediator;
        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Pagination<DepartmentDto>>> GetDepartments([FromQuery]DepartmentSpecParams departmentParams)
        {
            return Ok(await _mediator.Send(new GetAllDepartmentsQuery(departmentParams)));
        }
    }
}
