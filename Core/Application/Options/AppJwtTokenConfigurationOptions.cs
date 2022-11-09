namespace Application.Options
{
    public class AppJwtTokenConfigurationOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryInDays { get; set; }
    }
}
