using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DepartmentDto: BaseEntityDto
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
    }
}