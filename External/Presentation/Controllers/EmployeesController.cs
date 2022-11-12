using Application.Dtos;
using Application.EmployeeService.Commands;
using Application.EmployeeService.Queries;
using Application.Models.Page;
using Application.Types;
using Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Pagination<EmployeeDto>>> GetEmployees([FromQuery]EmployeeSpecParams employeeParams)
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery(employeeParams)));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {   
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
        }
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromForm] EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new InsertEmployeeCommand(employee) ));
        }
        [HttpGet("FormPageData")]
        [Authorize]
        public async Task<ActionResult<EmployeeFormPageModel>> GetEmployeeFormPageData()
        {
            return Ok(await _mediator.Send(new GetEmployeeFormPageQuery()));
        }
        [HttpPut]
        [Authorize]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromForm] EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new UpdateEmployeeCommand(employee)));
        }
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<int>> DeleteEmployee(EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand(employee)));
        }

    }
}
