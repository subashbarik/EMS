using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Options
{
    public class AppSmsOptions
    {
        [Required]
        public string AccountSid { get; set; }
        [Required]
        public string AuthToken { get; set; }
        [Required]
        public string From { get; set; }
    }
}
