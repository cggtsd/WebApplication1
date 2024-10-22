namespace WebApplication1.Models
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>> Placeholders { get; set; }
    }
}
