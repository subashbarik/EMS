using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Page
{
    public class EmployeeFormPageModel
    {
        public List<DepartmentDto> Departments { get; set; }
        public List<DesignationDto> Designations { get; set; }
        public string DefaultImageUrl { get; set; }
    }
}
