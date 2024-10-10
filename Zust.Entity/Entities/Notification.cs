using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entity.Entities
{
    public class Notification
    {
        public int Id { get; set; }  
        public string? SenderId { get; set; }  
        public string? ReceiverId { get; set; }  
        public string? Message { get; set; }  
        public DateTime CreatedAt { get; set; }  
        public bool IsRead { get; set; }  
        public string? ActionUrl { get; set; }
    }
}
