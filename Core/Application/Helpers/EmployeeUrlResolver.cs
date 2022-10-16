using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Options;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Application.Helpers
{
    public class EmployeeUrlResolver : IValueResolver<EmployeeDto, EmployeeDto, string>
    {   
        private readonly IOptions<AppConfigurationOptions> _options;
        public EmployeeUrlResolver(IOptions<AppConfigurationOptions> options)
        {   
            _options = options;
        }

        public string Resolve(EmployeeDto source, EmployeeDto destination, string destMember, ResolutionContext context)
        {  
             return _options.Value.ApiUrl + source.ImageUrl;
        }
    }
}