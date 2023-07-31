namespace Portfolio.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string ProjectUrl { get; set; }
        public List<string> Technologies { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public Project()
        {
            Technologies = new List<string>();
        }
    }
}