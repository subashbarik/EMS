using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class EmployeeWithDepartmentAndDesignationByIdSpec:BaseSpecification<Employee>
    {
        public EmployeeWithDepartmentAndDesignationByIdSpec(EmployeeSpecParams employeeParams):base(x => x.Id == employeeParams.Id)
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
        }
    }
}
