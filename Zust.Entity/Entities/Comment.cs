using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entity.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int LikeCount { get; set; }
        public string UserId { get; set; } // CustomUser'ın Id'si string
        public int? ReplyId { get; set; } // Yanıtlanan yorumun ID'si
        public int PostId { get; set; }

        public CustomUser? User { get; set; } 
       public Comment? Reply { get; set; }  
        public Post? Post { get; set; } 
    }
}
