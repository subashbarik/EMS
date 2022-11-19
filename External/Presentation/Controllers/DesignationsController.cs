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
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Pagination<DesignationDto>>> GetDeignations([FromQuery]DesignationSpecParams designationParams)
        {
            return Ok(await _mediator.Send(new GetAllDesignationsQuery(designationParams)));
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignationDto>> CreateDesignation([FromForm] DesignationDto designation)
        {
            return Ok(await _mediator.Send(new InsertDesignationCommand(designation)));
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DesignationDto>> UpdateDesignation([FromForm] DesignationDto designation)
        {
            return Ok(await _mediator.Send(new UpdateDesignationCommand(designation)));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> DeleteDesignation(DesignationDto designation)
        {
            return Ok(await _mediator.Send(new DeleteDesignationCommand(designation)));
        }
    }
}