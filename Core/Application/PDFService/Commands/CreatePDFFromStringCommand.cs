using MediatR;

namespace Application.PDFService.Commands
{
    public sealed record CreatePDFFromStringCommand(string Htmldata,string fileName):IRequest<bool>;
}