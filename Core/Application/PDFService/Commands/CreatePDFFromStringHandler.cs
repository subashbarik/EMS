using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Application.Interfaces;

namespace Application.PDFService.Commands
{
    public class CreatePDFFromStringHandler : IRequestHandler<CreatePDFFromStringCommand,bool>
    {
        private readonly IPDFGenerator _pdfGenerator;

        public CreatePDFFromStringHandler(IPDFGenerator pdfGenerator)
        {
            _pdfGenerator = pdfGenerator;
        }
        public async Task<bool> Handle(CreatePDFFromStringCommand request, CancellationToken cancellationToken)
        {
            return await _pdfGenerator.CreatePDFFromText(request.Htmldata, request.fileName);
        }
    }
}