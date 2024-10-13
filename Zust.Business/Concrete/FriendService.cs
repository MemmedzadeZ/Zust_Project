using Azure.Core;
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
        private readonly List<FriendRequest> _requests = new List<FriendRequest>(); // Sorğular üçün müvəqqəti yaddaş

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

        public void AddRequest(FriendRequest model)
        {
            model.Statust = "pending"; // Sorğu əlavə edilərkən statusu "pending" olmalıdır
            _requests.Add(model);
        }

        public async Task DeclineFriendRequest(int requestId)
        {
            await friendDal.DeclineFriendRequest(requestId);
        }

        public async Task<List<FriendRequest>> GetFriendRequests(string userId)
        {
           return await friendDal.GetFriendRequests(userId);    
        }

        public FriendRequest GetRequestById(int requestId)
        {
            throw new NotImplementedException();
        }

        public void UpdateRequest(FriendRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
