using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Options
{
    public class AppConfigurationOptions
    {
        public string ApiUrl { get; set; }
        public string EmployeeImagePath { get; set; }
        public int MaxImageSizeInKB { get; set; }
        public string NoImageEmployeePath { get; set; }
        public string LogoPath { get; set; }
        public string UIDateTimeFormat { get; set; }
    }
}
