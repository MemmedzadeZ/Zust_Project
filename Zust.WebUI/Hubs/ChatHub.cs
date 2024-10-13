using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Data;
using Zust.Entity.Data;
using Zust.Entity.Entities;

namespace Zust.WebUI.Hubs
{
    public class ChatHub:Hub
    {
        private readonly UserManager<CustomUser> _userManager;
        private IHttpContextAccessor _contextAccessor;
        private ZustDbContext _context;

        public ChatHub(UserManager<CustomUser> userManager, IHttpContextAccessor contextAccessor, ZustDbContext context)
        {
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (user != null)
            {
                var userItem = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if (userItem != null)
                {
                    userItem.IsOnline = true;
                    userItem.ConnectTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                    await _context.SaveChangesAsync();

                    string info = user.UserName + " connected successfully";
                    await Clients.Others.SendAsync("UserStatusChanged", user.Id, true); // Broadcast online status

                    await base.OnConnectedAsync();
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _userManager.GetUserAsync(_contextAccessor.HttpContext.User);
            if (user != null)
            {
                var userItem = _context.Users.SingleOrDefault(u => u.Id == user.Id);
                if (userItem != null)
                {
                    userItem.IsOnline = false;
                    userItem.ConnectTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                    await _context.SaveChangesAsync();

                    string info = user.UserName + " disconnected successfully";
                    await Clients.Others.SendAsync("UserStatusChanged", user.Id, false); // Broadcast offline status

                    await base.OnDisconnectedAsync(exception);
                }
            }
        }

     
        public async Task SendFollow(string id)
        {
            await Clients.User(id).SendAsync("ReceiveNotification");
        }
    }
}
