using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.FakeDataService.Commands;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    public class FakeDataController:BaseApiController
    {
        private readonly IMediator _mediator;
        public FakeDataController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Generates fake employee data for testing purpose
        /// </summary>
        /// <param name="numberOfRecords"></param>
        /// <returns></returns>
        [HttpPost("generatefakemployees")]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<string>> CreateFakeEmployees(int numberOfRecords)
        {
            return Ok(await _mediator.Send(new InsertFakeEmployeesCommand(numberOfRecords)));
        }
    }
}
