using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class SmsDto
    {
        [Required]
        public string To { get; set; }
        public string From { get; set; }
        public string Message { get; set; }
    }
}
