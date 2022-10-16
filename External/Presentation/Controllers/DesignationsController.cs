using Application.DesignationService.Queries;
using Application.Dtos;
using Application.Types;
using Domain.Specifications;
using MediatR;
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
        public async Task<ActionResult<Pagination<DesignationDto>>> GetEmployees([FromQuery]DesignationSpecParams designationParams)
        {
            return Ok(await _mediator.Send(new GetAllDesignationsQuery(designationParams)));
        }
    }
}