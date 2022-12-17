using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Types;
using Application.Dtos;
using Domain.Specifications;
using Application.DepartmentService.Queries;
using Microsoft.AspNetCore.Authorization;
using Application.DepartmentService.Commands;

namespace Presentation.Controllers
{
    public class DepartmentsController:BaseApiController
    {
        private readonly IMediator _mediator;
        public DepartmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a list of Departments
        /// </summary>
        /// <param name="departmentParams"></param>
        /// <returns></returns>
        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public async Task<ActionResult<Pagination<DepartmentDto>>> GetDepartments([FromQuery]DepartmentSpecParams departmentParams)
        {
            return Ok(await _mediator.Send(new GetAllDepartmentsQuery(departmentParams)));
        }
        /// <summary>
        /// Creates a department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<DepartmentDto>> CreateDepartment([FromForm] DepartmentDto department)
        {
            return Ok(await _mediator.Send(new InsertDepartmentCommand(department)));
        }
        /// <summary>
        /// Updates a department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<DepartmentDto>> UpdateDepartment([FromForm] DepartmentDto department)
        {
            return Ok(await _mediator.Send(new UpdateDepartmentCommand(department)));
        }
        /// <summary>
        /// Deletes a department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<int>> DeleteDepartment(DepartmentDto department)
        {
            return Ok(await _mediator.Send(new DeleteDepartmentCommand(department)));
        }
    }
}
