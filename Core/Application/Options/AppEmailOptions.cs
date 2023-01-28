using System.ComponentModel.DataAnnotations;

namespace Application.Options
{
    public class AppEmailOptions
    {
        [Required]
        public string Host { get; set; }
        [Required]
        public int Port { get; set; }
        [Required]
        public string FromEmail { get; set; }
        [Required]
        public string FromEmailPassword { get; set; }
    }
}
