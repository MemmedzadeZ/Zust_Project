using Zust.Entity.Entities;

namespace Zust.WebUI.Models
{
    public class AllFriendsViewModel
    {
        public int receiverId { get; set; }
        public int SenderId { get; set; } // Sorğu göndərənin ID-si
       
        public string Status { get; set; } // Sorğunun vəziyyəti (pending, accepted, rejected)

        public List<CustomUser>? Friends { get; set; }
    }
}