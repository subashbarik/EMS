using System.ComponentModel.DataAnnotations;

namespace Application.Options
{
    public class AppJwtTokenConfigurationOptions
    {
        [Required]
        public string Key { get; set; }
        [Required]
        public string Issuer { get; set; }
        [Required]
        public int ExpiryInDays { get; set; }
    }
}
