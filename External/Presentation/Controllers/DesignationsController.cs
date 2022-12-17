using Application.DesignationService.Commands;
using Application.DesignationService.Queries;
using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class DesignationsController : BaseApiController
    {
        private readonly IMediator _mediator;
        public DesignationsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a list of designations
        /// </summary>
        /// <param name="designationParams"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Pagination<DesignationDto>>> GetDeignations([FromQuery]DesignationSpecParams designationParams)
        {
            return Ok(await _mediator.Send(new GetAllDesignationsQuery(designationParams)));
        }
        /// <summary>
        /// Creates a designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignationDto>> CreateDesignation([FromForm] DesignationDto designation)
        {
            return Ok(await _mediator.Send(new InsertDesignationCommand(designation)));
        }
        /// <summary>
        /// Updates a designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignationDto>> UpdateDesignation([FromForm] DesignationDto designation)
        {
            return Ok(await _mediator.Send(new UpdateDesignationCommand(designation)));
        }
        /// <summary>
        /// Deletes a designation
        /// </summary>
        /// <param name="designation"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> DeleteDesignation(DesignationDto designation)
        {
            return Ok(await _mediator.Send(new DeleteDesignationCommand(designation)));
        }
    }
}