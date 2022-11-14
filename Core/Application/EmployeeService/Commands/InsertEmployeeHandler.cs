using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using Application.Options;

namespace Application.EmployeeService.Commands
{
    public class InsertEmployeeHandler : IRequestHandler<InsertEmployeeCommand, EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageProcessor _imgProcessor;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IOptions<AppConfigurationOptions> _options;

        public InsertEmployeeHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IImageProcessor imgProcessor,
            IWebHostEnvironment hostingEnvironment,
            IOptions<AppConfigurationOptions> options)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imgProcessor = imgProcessor;
            _hostingEnvironment = hostingEnvironment;
            _options = options;
        }
        public async Task<EmployeeDto> Handle(InsertEmployeeCommand request, CancellationToken cancellationToken)
        {
            EmployeeDto output = null;
            string fileName = "";
            bool fileSaved = true;
            var appCnfOptions = _options.Value;

            //Save image if provided
            if (request.employee.ImageFile != null)
            {
                fileName = Guid.NewGuid().ToString() + "_" + request.employee.FirstName + "_" + request.employee.LastName + "." + request.employee.ImageFile.FileName.Split('.')[1];
                var filePath = Path.Combine($"{appCnfOptions.EmployeeImagePath}", $"{fileName}");
                filePath = _hostingEnvironment.WebRootPath + filePath;
                fileSaved = await _imgProcessor.SaveImage(filePath, request.employee.ImageFile);
                request.employee.ImageUrl = appCnfOptions.EmployeeImagePath + "/" + fileName;
            }
            else
            {
                request.employee.ImageUrl = appCnfOptions.NoImageEmployeePath;
            }
            // Save other details
            if (fileSaved == true)
            {   
                var employee = _mapper.Map<EmployeeDto, Employee>(request.employee);
                _unitOfWork.Repository<Employee>().Add(employee);
                var retval = await _unitOfWork.Complete();
                if (retval > 0)
                {
                    output = _mapper.Map<Employee, EmployeeDto>(employee);
                }
            }

            return output;
        }
    }
}
