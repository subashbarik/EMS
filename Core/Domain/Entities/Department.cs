using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Department:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
