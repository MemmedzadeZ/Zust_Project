using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Zust.WebUI.Models;

using Zust.Entity.Entities;
using Zust.Business.Abstract;

namespace Zust.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<CustomUser> _userManager;
        private readonly RoleManager<CustomRole> _roleManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IImageService _imageService;

        // private readonly ZustDbContext zustDbContext;
        private readonly IUserService _userService;

        public AccountController(UserManager<CustomUser> userManager, RoleManager<CustomRole> roleManager, SignInManager<CustomUser> signInManager, IUserService userService, IImageService ımageService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            //   zustDbContext = context;
            _imageService = ımageService;
            _userService = userService;
        }



        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
          
            if (ModelState.IsValid)
            {
                if (vm.File != null)
                {
                    vm.ImageUrl = await _imageService.SaveFile(vm.File);
                }
                CustomUser user = new CustomUser
                {
                    UserName = vm.Username,
                    Email = vm.Email,
                    Image = vm.ImageUrl,
                };
                if (vm.Password == vm.ConfirmPassword)
                {
                    IdentityResult result = await _userManager.CreateAsync(user, vm.Password);
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync("Admin"))
                        {
                            CustomRole role = new CustomRole
                            {
                                Id = Guid.NewGuid().ToString(),
                                Name = "Admin"
                            };

                            IdentityResult roleResult = await _roleManager.CreateAsync(role);
                            if (!roleResult.Succeeded)
                            {
                                return View(vm);
                            }
                        }

                        await _userManager.AddToRoleAsync(user, "Admin");
                        return RedirectToAction("Login", "Account");
                    }
                }


            }
            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                currentUser.IsOnline = false;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetByUsernameOrEmail(model.UsernameOrEmail);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        user.ConnectTime = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                        user.IsOnline = true;
                        await _userService.Update(user);

                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid Login");
                }

            }
            return View(model);
        }

    }
}
