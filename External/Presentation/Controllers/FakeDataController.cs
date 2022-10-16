using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.FakeDataService.Commands;

namespace Presentation.Controllers
{
    public class FakeDataController:BaseApiController
    {
        private readonly IMediator _mediator;
        public FakeDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generatefakemployees")]
        public async Task<ActionResult<string>> CreateFakeEmployees(int numberOfRecords)
        {
            return Ok(await _mediator.Send(new InsertFakeEmployeesCommand(numberOfRecords)));
        }
    }
}
