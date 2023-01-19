using Application.Dtos;
using Application.Models.Page;
using Application.Options;
using Domain.Interfaces;
using Domain.Specifications;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.EmployeeService.Queries
{
    public class GetEmployeeFormPageHandler : IRequestHandler<GetEmployeeFormPageQuery, EmployeeFormPageModel>
    {
        private readonly IGenericDapperRepository _dapper;
        private readonly IOptions<AppConfigurationOptions> _options;

        public GetEmployeeFormPageHandler(IGenericDapperRepository dapper, IOptions<AppConfigurationOptions> options)
        {
            _dapper = dapper;
            _options = options;
        }
        public async Task<EmployeeFormPageModel> Handle(GetEmployeeFormPageQuery request, CancellationToken cancellationToken)
        {
            EmployeeFormPageModel output = new();
            var appCnfOptions = _options.Value;
            output.Departments = new List<DepartmentDto>();
            output.Designations = new List<DesignationDto>();

            DepartmentSpecParams deptSpecParams = new();
            var departments = await _dapper.LoadData<DepartmentDto, dynamic>("usp_GetAllDepartments",
                                               new
                                               {
                                                   Page = deptSpecParams.PageIndex,
                                                   Size = deptSpecParams.PageSize,
                                                   SortField = "Name",
                                                   SortType = deptSpecParams.Sort
                                               });
            output.Departments = departments;
            DesignationSpecParams desigSpecParams = new();
            var designations = await _dapper.LoadData<DesignationDto, dynamic>("usp_GetAllDesignations",
                                                new
                                                {
                                                    Page = desigSpecParams.PageIndex,
                                                    Size = desigSpecParams.PageSize,
                                                    SortField = "Name",
                                                    SortType = desigSpecParams.Sort
                                                });
            output.Designations = designations;
            var employeeTypes = await _dapper.LoadData<EmployeeTypeDto, dynamic>("usp_GetAllEmployeeTypes",
                                                new
                                                {
                                                    
                                                });                                    
            output.EmployeeTypes = employeeTypes;
            output.DefaultImageUrl = appCnfOptions.ApiUrl + appCnfOptions.NoImageEmployeePath;
            return output;
        }
    }
}
