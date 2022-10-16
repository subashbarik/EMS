using Application.Dtos;
using Application.Types;
using Domain.Interfaces;
using MediatR;


namespace Application.DepartmentService.Queries
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, Pagination<DepartmentDto>>
    {
        private readonly IGenericDapperRepository _dapper;
        
        public GetAllDepartmentsHandler(IGenericDapperRepository dapper)
        {
            _dapper = dapper;
        }
        public async Task<Pagination<DepartmentDto>> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            int totalDepartmentCount = await GetDepartmentCount();
            var departments = await GetDepartments(request);
            return new Pagination<DepartmentDto>(request.departmentParams.PageIndex,request.departmentParams.PageSize,
                                                totalDepartmentCount,departments);
        }
        private async Task<int> GetDepartmentCount()
        {
            int totalDepartmentCount = await _dapper.CountData("usp_GetDepartmentCount");
            return totalDepartmentCount;
        }
        private async Task<List<DepartmentDto>> GetDepartments(GetAllDepartmentsQuery request)
        {
            var departments = await _dapper.LoadData<DepartmentDto,dynamic>("usp_GetAllDepartments",
                                                new
                                                {
                                                    Page = request.departmentParams.PageIndex,
                                                    Size = request.departmentParams.PageSize,
                                                    SortField = "Name",
                                                    SortType = request.departmentParams.Sort
                                                });
            return departments; 
        }
    }
}