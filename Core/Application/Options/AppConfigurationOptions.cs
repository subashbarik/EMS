using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Options
{
    public class AppConfigurationOptions
    {
        [Required]
        public string ApiUrl { get; set; }
        [Required]
        public string EmployeeImagePath { get; set; }
        [Required]
        public int MaxImageSizeInKB { get; set; }
        [Required]
        public string NoImageEmployeePath { get; set; }
        [Required]
        public string LogoPath { get; set; }
        [Required]
        public string UIDateTimeFormat { get; set; }
    }
}
