using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Interfaces;
using Application.Options;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.EmployeeService.Commands
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericDapperRepository _dapper;
        private readonly IMapper _mapper;
        private readonly IImageProcessor _imgProcessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IOptions<AppConfigurationOptions> _options;

        public UpdateEmployeeHandler(
            IUnitOfWork unitOfWork,
            IGenericDapperRepository dapper,
            IMapper mapper,
            IImageProcessor imgProcessor,
            IWebHostEnvironment hostingEnvironment,
            IOptions<AppConfigurationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _dapper = dapper;
            _mapper = mapper;
            _imgProcessor = imgProcessor;
            _hostingEnvironment = hostingEnvironment;
            _options = options;
        }
        public async Task<EmployeeDto> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            EmployeeDto output = null;
            string fileName = "";
            bool fileSaved = true;
            var appCnfOptions = _options.Value;

            // Get the employee info from Database
            var empList = await _dapper.LoadData<Employee, dynamic>("usp_GetEmployeeById", new { Id = request.employee.Id });
            var dbEmp = empList.FirstOrDefault();
            //Save image if provided
            if (request.employee.ImageFile != null)
            {   
                //Delete the old image and make sure it is not the default image.
                if (!string.IsNullOrEmpty(dbEmp.ImageUrl) && dbEmp.ImageUrl != appCnfOptions.NoImageEmployeePath)
                {
                    _imgProcessor.DeleteImage(_hostingEnvironment.WebRootPath + dbEmp.ImageUrl);
                }

                //Save new image
                fileName = Guid.NewGuid().ToString() + "_" + request.employee.FirstName + "_" + request.employee.LastName + "." + request.employee.ImageFile.FileName.Split('.')[1];
                var filePath = Path.Combine($"{appCnfOptions.EmployeeImagePath}", $"{fileName}");
                filePath = _hostingEnvironment.WebRootPath + filePath;
                fileSaved = await _imgProcessor.SaveImage(filePath, request.employee.ImageFile);
                
                request.employee.ImageUrl = appCnfOptions.EmployeeImagePath + "/" + fileName;
            }
            else
            {
                // Preserve image 
                if(!request.employee.RemoveImage)
                {
                    request.employee.ImageUrl = dbEmp.ImageUrl;
                }
                
            }
            // Update other details
            if (fileSaved == true)
            {
                var employee = _mapper.Map<EmployeeDto, Employee>(request.employee);
                _unitOfWork.Repository<Employee>().Update(employee);
                var retval = await _unitOfWork.Complete();
                if (retval > 0)
                {
                    EmployeeSpecParams empSpecParams = new()
                    {
                        Id = employee.Id
                    };
                    var spec = new EmployeeWithDepartmentAndDesignationByIdSpec(empSpecParams);
                    var employeeWithDeptandDesig = await _unitOfWork.Repository<Employee>().GetEntityWithSpecAsync(spec);
                    output = _mapper.Map<Employee, EmployeeDto>(employeeWithDeptandDesig);
                }
            }

            return output;

            
        }
    }
}
