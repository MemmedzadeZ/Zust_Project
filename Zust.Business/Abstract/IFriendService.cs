using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entity.Entities;

namespace Zust.Business.Abstract
{
   public interface IFriendService
    {
        Task? Add(FriendRequest friend);
        Task<List<FriendRequest>> GetFriendRequests(string userId);
        Task AcceptFriendRequest(int requestId);
        Task DeclineFriendRequest(int requestId);
    }
}
