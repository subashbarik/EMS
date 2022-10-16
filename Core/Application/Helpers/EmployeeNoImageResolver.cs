using Application.Dtos;
using Application.Options;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class EmployeeNoImageResolver : IValueResolver<EmployeeDto, Employee, string>
    {   
        private readonly IOptions<AppConfigurationOptions> _options;
        public EmployeeNoImageResolver(IOptions<AppConfigurationOptions> options)
        {
            
            _options = options;
        }
        public string Resolve(EmployeeDto source, Employee destination, string destMember, ResolutionContext context)
        {
            if (String.IsNullOrEmpty(source.ImageUrl))
            {
                return _options.Value.NoImageEmployeePath;
            }
            return source.ImageUrl;
        }
    }
}
