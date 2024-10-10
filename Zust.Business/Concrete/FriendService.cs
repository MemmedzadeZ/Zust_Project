using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entity.Entities;

namespace Zust.Business.Concrete
{
    public class FriendService : IFriendService
    {
        private readonly IFriendDal friendDal;

        public FriendService(IFriendDal friendDal)
        {
            this.friendDal = friendDal;
        }

        public async Task AcceptFriendRequest(int requestId)
        {
            await friendDal.AcceptFriendRequest(requestId); 
        }

        public async Task? Add(FriendRequest friend)
        {
        await friendDal.Add(friend);
        }

        public async Task DeclineFriendRequest(int requestId)
        {
            await friendDal.DeclineFriendRequest(requestId);
        }

        public async Task<List<FriendRequest>> GetFriendRequests(string userId)
        {
           return await friendDal.GetFriendRequests(userId);    
        }
    }
}
