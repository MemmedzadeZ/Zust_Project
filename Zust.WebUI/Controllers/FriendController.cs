using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.Business.Abstract;
using Zust.Entity.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    public class FriendController : Controller
    {
        private readonly ILogger<FriendController> _logger;
        private readonly UserManager<CustomUser> _userManager;
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;

        public FriendController(ILogger<FriendController> logger, UserManager<CustomUser> userManager, IUserService userService, IFriendService friendService)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _friendService = friendService;
        }
       
    }
}
