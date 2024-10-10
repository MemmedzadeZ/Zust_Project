using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
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
                    await _context.SaveChangesAsync();
                }
            }

            await base.OnConnectedAsync();

            //   string info = user.UserName + " connected successfully";
            //  await Clients.Others.SendAsync("Connect", info);
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
                    await _context.SaveChangesAsync();
                }
            }

            await base.OnDisconnectedAsync(exception);

            //  string info = user.UserName + " diconnected successfully";
            //  await Clients.Others.SendAsync("Disconnect", info);
        }

        public async Task SendFollow(string id)
        {
            await Clients.User(id).SendAsync("ReceiveNotification");
        }
    }
}
