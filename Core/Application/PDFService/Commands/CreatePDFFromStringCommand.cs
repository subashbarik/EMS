using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.PDFService.Commands
{
    public sealed record CreatePDFFromStringCommand(string Htmldata,string fileName):IRequest<bool>;
}