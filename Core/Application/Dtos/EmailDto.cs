using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class EmailDto:BaseDto
    {
        [Required]
        [EmailAddress]
        public List<string> To { get; set; }
        [EmailAddress]
        public List<string> CC { get; set; }
        [EmailAddress]
        public List<string> BCC { get; set; }
        [EmailAddress]
        public string From { get; set; }
        public string Password { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
