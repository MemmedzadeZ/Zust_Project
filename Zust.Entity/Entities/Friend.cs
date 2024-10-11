using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zust.Entity.Entities
{
    public class Friend
    {

        public int Id { get; set; }

        public string? OwnId {  get; set; }
        public string? YourFriendId { get; set; }

        public virtual CustomUser? YourFriend {  get; set; }
    }
}
