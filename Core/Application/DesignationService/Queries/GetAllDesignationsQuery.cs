using MediatR;
using Application.Dtos;
using Application.Types;
using Domain.Specifications;
namespace Application.DesignationService.Queries
{
    public record GetAllDesignationsQuery(DesignationSpecParams designationParams):IRequest<Pagination<DesignationDto>>;
   
}