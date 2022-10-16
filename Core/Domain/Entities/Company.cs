using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }

        public List<Department> Departments { get; set; }
    }
}
