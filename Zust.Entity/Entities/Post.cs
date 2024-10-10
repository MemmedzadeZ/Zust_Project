using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entity.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public string? ImageUrl { get; set; } 
        public string? VideoUrl { get; set; } 

        public string Status { get; set; } = "public";
        public string UserId { get; set; }
        public int LikeCount { get; set; } = 0;
        public DateTime CreatedDate { get; set; }  = DateTime.Now;  
        public DateTime? UpdatedDate { get; set; }

        public List<Comment>? Comments { get; set; }
        public CustomUser User { get; set; }


    }
}
