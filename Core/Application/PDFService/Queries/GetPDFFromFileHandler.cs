using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PDFService.Queries
{
    public class GetPDFFromFileHandler : IRequestHandler<GetPDFFromFileQuery, FileStreamResult>
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public GetPDFFromFileHandler(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<FileStreamResult> Handle(GetPDFFromFileQuery request, CancellationToken cancellationToken)
        {
            string dir = _hostingEnvironment.WebRootPath + "\\PDF";
            string path = Path.Combine(dir, request.FileName);
            var stream = new FileStream(path, FileMode.Open);
            return await Task.FromResult(new FileStreamResult(stream, "application/pdf"));
        }

       
    }
}
