using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class EmployeesWithDepartmentAndDesignationSpecification : BaseSpecification<Employee>
    {
        public EmployeesWithDepartmentAndDesignationSpecification(EmployeeSpecParams employeeParams):base(
            x =>
            (string.IsNullOrEmpty(employeeParams.Search) || x.FirstName.ToLower().Contains(employeeParams.Search)) &&
            (!employeeParams.DepartmentId.HasValue || x.DepartmentId == employeeParams.DepartmentId) &&
            (!employeeParams.DesignationId.HasValue || x.DesignationId == employeeParams.DesignationId)
        )
        {
            AddInclude(x => x.Department);
            AddInclude(x => x.Designation);
            AddOrderBy(n => n.FirstName);
            ApplyPaging(employeeParams.PageSize * (employeeParams.PageIndex - 1), employeeParams.PageSize);

            if (!String.IsNullOrEmpty(employeeParams.Sort))
            {
                switch (employeeParams.Sort)
                {
                    case "salaryAsc":
                        AddOrderBy(e => e.Basic);
                        break;
                    case "salaryDesc":
                        AddOrderByDescending(e => e.Basic);
                        break;
                    default:
                        AddOrderBy(n => n.FirstName);
                        break;
                }
            }
        }
    }
}
