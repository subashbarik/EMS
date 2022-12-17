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
        /// <summary>
        /// Initializes some global data such as config etc for the client application.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<GlobalModel>> GetGlobalAppData()
        {
            return Ok(await _mediator.Send(new GetGlobalsQueries()));
        }
    }
}
