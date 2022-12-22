namespace Application.Dtos
{
    public class EmailDto:BaseDto
    {
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public List<string> BCC { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
