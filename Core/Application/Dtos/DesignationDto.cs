using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class DesignationDto: BaseEntityDto
    {   
        public string Name { get; set; }
        public string Description { get; set; }
        public double Basic { get; set; }
        public double TAPercentage { get; set; }
        public double DAPercentage { get; set; }
        public double HRAPercentage { get; set; }
        
    }
}