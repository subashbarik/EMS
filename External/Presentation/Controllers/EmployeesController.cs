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
        /// <summary>
        /// Gets a list of employees
        /// </summary>
        /// <param name="employeeParams"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Pagination<EmployeeDto>>> GetEmployees([FromQuery]EmployeeSpecParams employeeParams)
        {
            return Ok(await _mediator.Send(new GetAllEmployeeQuery(employeeParams)));
        }
        /// <summary>
        /// Gets the details of a employee based on the passed in id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeById(int id)
        {   
            return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));
        }
        /// <summary>
        /// Creates an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromForm] EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new UpdateEmployeeCommand(employee)));
        }
        /// <summary>
        /// Deletes an employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<int>> DeleteEmployee(EmployeeDto employee)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand(employee)));
        }

    }
}
