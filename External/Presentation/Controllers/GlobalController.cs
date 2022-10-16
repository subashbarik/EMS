using Application.GlobalService.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Presentation.Controllers
{
    public class GlobalController:BaseApiController
    {
        private readonly IMediator _mediator;

        public GlobalController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<GlobalModel>> GetGlobalAppData()
        {
            return Ok(await _mediator.Send(new GetGlobalsQueries()));
        }
    }
}
