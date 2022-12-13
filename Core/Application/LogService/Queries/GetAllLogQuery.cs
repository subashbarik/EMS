using MediatR;
using Application.Dtos;
using Application.Types;
using Domain.Specifications;

namespace Application.LogService.Queries
{
    public sealed record GetAllLogQuery(LogSpecParams LogParams):IRequest<Pagination<LogDto>>;
}
