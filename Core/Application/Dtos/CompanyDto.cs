using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class CompanyDto: BaseEntityDto
    {   
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        
    }
}