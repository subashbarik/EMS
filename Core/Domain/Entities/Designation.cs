using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Designation:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Basic { get; set; }
        public double TAPercentage { get; set; }
        public double DAPercentage { get; set; }
        public double HRAPercentage { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
