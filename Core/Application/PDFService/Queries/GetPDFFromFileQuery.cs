using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.PDFService.Queries
{
    public sealed record GetPDFFromFileQuery(string FileName):IRequest<FileStreamResult>;
}
