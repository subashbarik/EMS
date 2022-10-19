namespace Application.Options
{
    public class AppTokenConfigurationOptions
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public int ExpiryInDays { get; set; }
    }
}
