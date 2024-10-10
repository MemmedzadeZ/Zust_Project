using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Zust.Business.Abstract;
using Zust.Entity.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.ViewComponents
{
    public class ContactViewComponent : ViewComponent
    {
        private readonly IUserService userService;
        private readonly UserManager<CustomUser> _userManager;

        public ContactViewComponent(IUserService userService,UserManager<CustomUser> userManager)
        {
            this.userService = userService;
            this._userManager = userManager;    
        }

        public   ViewViewComponentResult Invoke()
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;
          

            var contacts = userService.GetAll(user.Id);
            return View(new AllFriendsViewModel
            {
                Friends =   contacts.Result,
            });
        }
    }
}
