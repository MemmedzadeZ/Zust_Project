namespace Zust.WebUI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; } 
        public string? Description { get; set; }
        public string? Tag { get; set; } 
        public IFormFile? FormFileImage { get; set; }
        public IFormFile? FormFileVideo { get; set; }
        public string? ImageUrl { get; set; } = null;
        public string? VideoUrl { get; set; }= null;
        public string Status { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
