namespace Portfolio.Models
{
    public class AboutMe
    {
        public string Id { get; set; }
        public string Language { get; set; } // Represents the language (e.g., "English" or "French")
        public string AboutText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
