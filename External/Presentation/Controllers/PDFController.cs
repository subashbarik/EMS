using Microsoft.AspNetCore.Mvc;
using Application.PDFService.Commands;
using MediatR;
using Application.PDFService.Queries;

namespace Presentation.Controllers
{
    public class PDFController:BaseApiController
    {
       private readonly IMediator _mediator;
        public PDFController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Creates a PDF file from the HTML string with the provided file name
        /// </summary>
        /// <param name="htmlData"></param>
        /// <param name="fileName"></param>
        /// <returns>Returns bool</returns>
        [HttpPost("createpdffromstring")]
        public async Task<ActionResult<bool>> CreatePDFFromString([FromQuery] string htmlData, string fileName)
        {
            return Ok(await _mediator.Send(new CreatePDFFromStringCommand(htmlData,fileName)));
        }
        /// <summary>
        /// Returns PDF data for the passed in pdf file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("getpdffromfile")]
        public async Task<FileStreamResult> GetPDFAsStream([FromQuery] string filename)
        {
            return await _mediator.Send(new GetPDFFromFileQuery(filename));
        }
    }
}