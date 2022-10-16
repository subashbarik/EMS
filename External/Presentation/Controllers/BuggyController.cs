using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;

namespace Presentation.Controllers
{
    public class BuggyController : BaseApiController
    {
        // private readonly EMSContext _context;
        // public BuggyController(EMSContext context)
        // {
        //     _context = context;
        // }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }

        // [HttpGet("notfound")]
        // public ActionResult GetNotFoundRequest()
        // {
        //     var thing = _context.Employee.Find(42);

        //     if (thing == null) return NotFound(new ApiResponse(404));

        //     return Ok();
        // }

        // [HttpGet("servererror")]
        // public ActionResult GetServerError()
        // {
        //     var thing = _context.Employee.Find(42);

        //     var thingToReturn = thing.ToString();

        //     return Ok();
        // }

        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        [HttpGet("indexoutofbound")]
        public ActionResult IndexOutOfBound()
        {
            int data;
            int[] test = { 1, 2, 3 };
            data = test[4];
            return Ok(data);
        }
        
    }
}