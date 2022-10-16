using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class EmployeesWithFiltersForCountSpecification : BaseSpecification<Employee>
    {
        public EmployeesWithFiltersForCountSpecification(EmployeeSpecParams employeeParams) : base(
             x =>
             (string.IsNullOrEmpty(employeeParams.Search) || x.FirstName.ToLower().Contains(employeeParams.Search)) &&
             (!employeeParams.DepartmentId.HasValue || x.DepartmentId == employeeParams.DepartmentId) &&
             (!employeeParams.DesignationId.HasValue || x.DesignationId == employeeParams.DesignationId)
         )
        {

        }
    }
}
