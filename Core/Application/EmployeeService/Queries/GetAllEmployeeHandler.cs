using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Application.Dtos;
using Domain.Specifications;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Application.Types;
using Domain.Types;

namespace Application.EmployeeService.Queries
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeQuery, Pagination<EmployeeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericDapperRepository _dapper;
        private readonly ILogger<GetAllEmployeeHandler> _logger;

        public GetAllEmployeeHandler(IUnitOfWork unitOfWork, IMapper mapper,IGenericDapperRepository dapper,ILogger<GetAllEmployeeHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dapper = dapper;
            _logger = logger;
        }
        //public async Task<Pagination<EmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    var spec = new EmployeesWithDepartmentAndDesignationSpecification(request.employeeParams);
        //    var countSpec = new EmployeesWithFiltersForCountSpecification(request.employeeParams);

        //    var totalEmployeeCount = await _unitOfWork.Repository<Employee>().CountAsync(countSpec);
        //    var employees = await _unitOfWork.Repository<Employee>().ListAsync(spec);
        //    stopwatch.Stop();
        //    _logger.LogInformation("Elapsed Time is {0} ms", stopwatch.ElapsedMilliseconds);
        //    var data = _mapper.Map<IReadOnlyList<Employee>, IReadOnlyList<EmployeeDto>>(employees);
        //    return new Pagination<EmployeeDto>(request.employeeParams.PageIndex, request.employeeParams.PageSize, totalEmployeeCount, data);
        //}
        public async Task<Pagination<Dtos.EmployeeDto>> Handle(GetAllEmployeeQuery request, CancellationToken cancellationToken)
        {   
            int totalEmployeeCount = await GetEmployeeCount();
            var employees = await GetEmployees(request);
            var data = TransformData(employees);
            return new Pagination<EmployeeDto>(request.employeeParams.PageIndex, request.employeeParams.PageSize, totalEmployeeCount, data);
        }
        private async Task<int> GetEmployeeCount()
        {
            int totalEmployeeCount = await _dapper.CountData("usp_GetEmployeeCount");
            return totalEmployeeCount;
        }
        private async Task<List<EmployeeDto>> GetEmployees(GetAllEmployeeQuery request)
        {
            var employees = await _dapper.LoadData<EmployeeDto, dynamic>("usp_GetAllEmployees",
                                                new
                                                {
                                                    Page = request.employeeParams.PageIndex,
                                                    Size = request.employeeParams.PageSize,
                                                    SortField = "FirstName",
                                                    SortType = request.employeeParams.Sort
                                                });
            
            
            return employees;
        }
        private IReadOnlyList<EmployeeDto> TransformData(List<EmployeeDto> employees)
        {
            var data = _mapper.Map<IReadOnlyList<EmployeeDto>, IReadOnlyList<EmployeeDto>>(employees);
            return data;
        }
    }
}
