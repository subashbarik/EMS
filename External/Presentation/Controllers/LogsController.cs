using Application.Dtos;
using Application.EmployeeService.Queries;
using Application.LogService.Queries;
using Application.Types;
using Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class LogsController:BaseApiController
    {
        private readonly IMediator _mediator;

        public LogsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Pagination<LogDto>>> GetLogs([FromQuery] LogSpecParams logParams)
        {
            return Ok(await _mediator.Send(new GetAllLogQuery(logParams)));
        }
    }
}
