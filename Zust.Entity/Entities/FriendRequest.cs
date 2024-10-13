using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entity.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public string? Content {  get; set; }
        public string? Statust { get; set; }

        public string? SenderId { get; set; } 
        public string? ReceiverId { get; set; } 
        public bool IsAccepted { get; set; } 
        public virtual CustomUser? Sender { get; set; }   
      
    }
}
