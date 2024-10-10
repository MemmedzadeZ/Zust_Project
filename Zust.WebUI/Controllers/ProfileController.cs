using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entity.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IPostService _postService;
        private readonly IImageService _imageService;
        private readonly IUserService _userService;


        public ProfileController(UserManager<CustomUser> userManager, IPostService postService, IImageService imageService, ILogger<ProfileController> logger, IUserService userService, SignInManager<CustomUser> signInManager)
        {
            _userManager = userManager;
            _postService = postService;
            _imageService = imageService;
            _logger = logger;
            _userService = userService;
            _signInManager = signInManager;
        }

      


    }
}

