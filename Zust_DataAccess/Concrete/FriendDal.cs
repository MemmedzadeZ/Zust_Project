using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Zust.DataAccess.Abstract;
using Zust.Entity.Data;
using Zust.Entity.Entities;

namespace Zust.DataAccess.Concrete
{
    public class FriendDal : IFriendDal
    {
        private readonly ZustDbContext zustDb;
        private readonly UserManager<CustomUser> userManager;   

        public FriendDal(ZustDbContext zustDb,UserManager<CustomUser> userManager)
        {
            this.zustDb = zustDb;
            this.userManager = userManager;
        }

        public async Task AcceptFriendRequest(int requestId)
        {
            var friendRequest = await zustDb.FriendRequests.FindAsync(requestId);
            if (friendRequest != null)
            {
                friendRequest.IsAccepted = true;
                await zustDb.SaveChangesAsync();
            }
        }

        public async Task? Add(FriendRequest friend)
        {
          await zustDb.AddAsync(friend);
            await zustDb.SaveChangesAsync();
        }

        public async Task DeclineFriendRequest(int requestId)
        {
            var friendRequest = await zustDb.FriendRequests.FindAsync(requestId);
            if (friendRequest != null)
            {
                zustDb.FriendRequests.Remove(friendRequest);
                await zustDb.SaveChangesAsync();
            }
        }

        public async Task<List<FriendRequest>> GetFriendRequests(string userId)
        {
           return await zustDb.FriendRequests.Where(f=>f.ReceiverId == userId && !f.IsAccepted).ToListAsync();
        }

        public async Task<List<FriendRequest>> GetFriendsAsync(string senderId)
        {
            return await zustDb.FriendRequests.Where(f => f.SenderId != senderId).ToListAsync();
        }
    }
}
