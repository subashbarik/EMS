using Application.Dtos;
using Application.Types;
using Domain.Interfaces;
using MediatR;

namespace Application.DesignationService.Queries
{
    public class GetAllDesignationsHandler : IRequestHandler<GetAllDesignationsQuery, Pagination<DesignationDto>>
    {
        private readonly IGenericDapperRepository _dapper;
        public GetAllDesignationsHandler(IGenericDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public async Task<Pagination<DesignationDto>> Handle(GetAllDesignationsQuery request, CancellationToken cancellationToken)
        {
            int totalDesignationCount = await GetDesignationCount();
            var designations = await GetDesignations(request);
            return new Pagination<DesignationDto>(request.designationParams.PageIndex,request.designationParams.PageSize,
                                                totalDesignationCount,designations);
        }
        private async Task<int> GetDesignationCount()
        {
            int totalDesignationCount = await _dapper.CountData("usp_GetDesignationCount");
            return totalDesignationCount;
        }
        private async Task<List<DesignationDto>> GetDesignations(GetAllDesignationsQuery request)
        {
            var designations = await _dapper.LoadData<DesignationDto,dynamic>("usp_GetAllDesignations",
                                                new
                                                {
                                                    Page = request.designationParams.PageIndex,
                                                    Size = request.designationParams.PageSize,
                                                    SortField = "Name",
                                                    SortType = request.designationParams.Sort
                                                });
            return designations; 
        }
    }
    
}