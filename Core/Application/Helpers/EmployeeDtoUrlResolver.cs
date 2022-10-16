using Application.Dtos;
using Application.Options;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class EmployeeDtoUrlResolver : IValueResolver<Employee, EmployeeDto, string>
    {
        private readonly IOptions<AppConfigurationOptions> _options;

        public EmployeeDtoUrlResolver(IOptions<AppConfigurationOptions> options)
        {
            _options = options;
        }
        public string Resolve(Employee source, EmployeeDto destination, string destMember, ResolutionContext context)
        {
            return _options.Value.ApiUrl + source.ImageUrl;
        }
    }
}
