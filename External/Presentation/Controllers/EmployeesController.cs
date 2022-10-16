﻿using Application.Dtos;
using Application.EmployeeService.Commands;
using Application.EmployeeService.Queries;
using Application.Models.Page;
using Application.Types;
using Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Presentation.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public EmployeesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<EmployeeDto>>> GetEmployees([FromQuery]EmployeeSpecParams employeeParams)
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery(employeeParams)));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {   
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
        }
        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromForm] EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new InsertEmployeeCommand(employee) ));
        }
        [HttpGet("FormPageData")]
        public async Task<ActionResult<EmployeeFormPageModel>> GetEmployeeFormPageData()
        {
            return Ok(await _mediator.Send(new GetEmployeeFormPageQuery()));
        }
        [HttpPut]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromForm] EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new UpdateEmployeeCommand(employee)));
        }
        [HttpDelete]
        public async Task<ActionResult<int>> DeleteEmployee(EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand(employee)));
        }

    }
}
