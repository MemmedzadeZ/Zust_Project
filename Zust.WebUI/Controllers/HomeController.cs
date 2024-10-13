using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Zust.Business.Abstract;
using Zust.Business.Concrete;
using Zust.Entity.Data;
using Zust.Entity.Entities;
using Zust.WebUI.Models;

namespace Zust.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger; 
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IFriendService _friendService;
        private readonly IPostService _postService;
        private readonly IImageService _imageService;
        private readonly ZustDbContext _context;
        //private readonly INotificationService _notificationService;


        public HomeController(ILogger<HomeController> logger, UserManager<CustomUser> userManager, IUserService userService, IFriendService friendService, IPostService postService, IImageService imageService, SignInManager<CustomUser> signInManager, ZustDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _userService = userService;
            _friendService = friendService;
            _postService = postService;
            _imageService = imageService;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }
        public async Task<IActionResult> Friends()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;

            var users = _userService.GetAll(user.Id);
            var model = new AllFriendsViewModel
            {
                Friends = await users
            };
            return View(model);
        }

        
        public async Task SendFriendRequest(string receiverId)
        {
            var senderId = _userManager.GetUserId(HttpContext.User);

            var friendRequest = new FriendRequest
            {
                SenderId = senderId,
                ReceiverId = receiverId, 
                IsAccepted = false
            };

            await _friendService.Add(friendRequest);
         }

     
    
        //[HttpPost]
        //public IActionResult SendRequest(AllFriendsViewModel model)
        //{
        //    // Sorğu məlumatlarını "pending" statusu ilə qeyd edirik.
        //    model.Status = "pending";

        //    // Verilənlər bazasına sorğu əlavə edilir və ya hansısa xidmət vasitəsilə sorğu göndərilir.
        //    // Məsələn:
        //    _friendService.AddRequest(model);

        //    // Alıcıya bildiriş göndər (simulyasiya kimi əlavə edə bilərsən).
        //    // Bildiriş göndərmə funksiyası:
        //    _friendService.NotifyUser(model.receiverId, "Yeni sorğu gəldi. Qəbul et və ya rədd et.");

        //    return RedirectToAction("PendingRequests"); // Sorğu göndərildikdən sonra "pending" səhifəsinə yönəldirik.
        //}

        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            var model = new ProfileViewModel
            {
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                AboutMe = user.AboutMe,
                Address = user.Address,
                Birthday = user.Birthday,
                City = user.City,
                ConnectTime = user.ConnectTime,
                DisConnectTime = user.DisConnectTime,
                Blood = user.Blood,
                Gender = user.Gender,
                Occupation = user.Occupation,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Language = user.Language,
                RelationStatus = user.RelationStatus,
            };

            var posts = await _postService.GetPosts();
            var postList = posts.Where(p => p.UserId == user.Id).ToList();
            var list = new PostAndProfileListViewModel
            {
                ProfileView = model,
                MyPostList = postList
            };
            return View(list);
        }

        //public async Task<IActionResult> Friends()
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    ViewBag.User = user; 

        //    var users=_userService.GetAll();
        //    var model = new AllFriendsViewModel
        //    {
        //        Friends =await users
        //    };
        //    return View(model);S
        //}
        public async Task<IActionResult> Index()
        {
            var user=await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User=user;
            return View();
        }
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    try
        //    {
        //        var user = await _userManager.GetUserAsync(HttpContext.User);

        //        var users = await _userService.GetAll(user.Id);
        //        return Ok(users);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error fetching users.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}
        public async Task<IActionResult> GetAllUsers()
        {
         
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var myrequests = _context.FriendRequests.Where(r => r.SenderId == user.Id);

        
            var datas = await _userService.GetAll(user.Id);



            var users = await _context.Users
                .Where(u => u.Id != user.Id)
                .OrderByDescending(u => u.IsOnline)
                .Select(u => new CustomUser
                {
                    Id = u.Id,
                    IsOnline = u.IsOnline,
                    UserName = u.UserName,
                    Image = u.Image,
                    Email = u.Email,
                    HasRequestPending = (myrequests.FirstOrDefault(r => r.ReceiverId == u.Id && r.Statust == "Request")!= null)
                })
            .ToListAsync();

            return Ok(users);
            }
        public async Task<IActionResult> SendFollow(string id)
        {
            var sender = await _userManager.GetUserAsync(HttpContext.User);
            var receiverUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(receiverUser != null)
            {
                _context.FriendRequests.Add(new FriendRequest
                {
                    Content = $"{sender.UserName} sent friend request at {DateTime.Now.ToLongDateString()}",
                    SenderId = sender.Id,
                    Sender = sender,
                    ReceiverId = id,
                    Statust = "Request"

                });
                await _context.SaveChangesAsync();
                return Ok();

               
            }

            return BadRequest();
        }
 

        public async Task<IActionResult> GetAllRequests()
        {

            var current = await _userManager.GetUserAsync(HttpContext.User);
            var requests = _context.FriendRequests.Where(r => r.ReceiverId == current.Id);
            return Ok(requests);
        }

        public async Task<IActionResult> Weather()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

      
  
        public async Task<IActionResult> Messages()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }

        //Notification


        //[HttpPost]
        //public async Task<IActionResult> DeleteNotification(int notificationId)
        //{
        //    try
        //    {
        //        await _notificationService.DeleteNotification(notificationId);
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error deleting notification.");
        //        return StatusCode(500, "Internal server error.");
        //    }
        //}

        // Action to handle edit, hide, delete from dropdown menu
      

        //public async Task<IActionResult> Notifications()
        //{
        //    var user = await _userManager.GetUserAsync(HttpContext.User);
        //    ViewBag.User = user;

        //    // Fetch notifications from the database for the current user
        //    var notifications = await _notificationService.GetUserNotifications(user.Id);
        //    return View(notifications);
        //}
        public async Task<IActionResult> Notifications()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;
            return View();
        }




        public async Task<IActionResult> Setting()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;


            var model = new ProfileViewModel
            {
                Username = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Birthday = user.Birthday,
                Gender = user.Gender,
                Occupation = user.Occupation,
                PhoneNumber = user.PhoneNumber,
                RelationStatus = user.RelationStatus,
                City = user.City,
                Country = user.Country,
                Address = user.Address,
                Blood = user.Blood,
                Email = user.Email,
                Language = user.Language,

            };
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ProfileInfo(ProfileViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                try
                {
                    user.PhoneNumber = model.PhoneNumber ?? "";
                    user.Birthday = model.Birthday;
                    user.Gender = model.Gender ?? "";
                    user.Occupation = model.Occupation ?? "";
                    user.RelationStatus = model.RelationStatus ?? "";
                    user.City = model.City ?? "";
                    user.Country = model.Country ?? "";
                    user.Address = model.Address ?? "";
                    user.Blood = model.Blood ?? "";
                    user.Email = model.Email ?? "";
                    user.Firstname = model.Firstname ?? "";
                    user.Lastname = model.Lastname ?? "";
                    user.Language = model.Language ?? "";

                    await _userService.Update(user);
                    await _userManager.UpdateAsync(user);

                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "User not found" });
        }



        [HttpPost]
        public async Task<IActionResult> AccountSetting(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (user != null)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(model.Fullname))
                        {
                            var names = model.Fullname.Split(" ");
                            var firstName = names[0];
                            var lastName = names[1];
                            user.Firstname = firstName;
                            user.Lastname = lastName;
                        }

                        user.UserName = model.Username;
                        user.PhoneNumber = model.PhoneNumber;
                        user.Country = model.Country;
                        user.Email = model.Email;

                        await _userService.Update(user);
                        await _userManager.UpdateAsync(user);

                        return Json(new { success = true });
                    }


                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return Json(new { success = false, message = ex.Message });
                    }
                }
            }

            return Json(new { success = false, message = "User not found" });
        }




        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (model.FormFileImage != null)
            {
                model.ImageUrl = await _imageService.SaveFile(model.FormFileImage);
            }
            if (model.FormFileVideo != null)
            {
                model.VideoUrl = await _imageService.SaveFile(model.FormFileVideo);
            }


            var newPost = new Post
            {
                Description = model.Description,
                Tag = model.Tag,
                ImageUrl = model.ImageUrl,
                VideoUrl = model.VideoUrl,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,

            };
            await _postService.Add(newPost);
            //return Json(new { success = true, message = "Post uploaded successfully!" });
            return PartialView("_PostPartial", new PostPartialViewModel { Post = newPost });

        }

        [HttpPost]
        public async Task<ActionResult> CloseAccount(ProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                var isPasswordCorrect = await _userManager.CheckPasswordAsync(currentUser, model.Password);
                if (isPasswordCorrect && model.Email == currentUser.Email)
                {
                    await _userService.Remove(currentUser);
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("Setting");

        }
        [HttpPost]
        public async Task<ActionResult> ChangePassword(ProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null)
            {
                try
                {
                    var isPasswordCorrect = await _userManager.CheckPasswordAsync(currentUser, model.Password);
                    if (isPasswordCorrect && model.NewPassword == model.ChangePassword)
                    {
                        await _userManager.ChangePasswordAsync(currentUser, model.Password, model.NewPassword);
                        return Json(new { success = true });
                    }
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = ex.Message });
                }
            }


            return Json(new { success = false, message = "User not found" });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeImageUrl(ProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null && model.FormFile != null && model.FormFile.Length > 0)
            {

                var imageUrl = await _imageService.SaveFile(model.FormFile);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    currentUser.Image = imageUrl;
                    await _userService.Update(currentUser);
                    await _userManager.UpdateAsync(currentUser);

                    return RedirectToAction("MyProfile");
                }
            }

            return RedirectToAction("MyProfile", model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeCoverImage(ProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null && model.FormFile != null && model.FormFile.Length > 0)
            {
                // Resmi kaydet
                var imageUrl = await _imageService.SaveFile(model.FormFile);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    // Kullanıcının resmini güncelle
                    currentUser.BackgroundImage = imageUrl;
                    await _userService.Update(currentUser);
                    await _userManager.UpdateAsync(currentUser);

                    return RedirectToAction("MyProfile");
                }
            }

            return RedirectToAction("MyProfile", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutMe(ProfileViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            if (currentUser != null && !string.IsNullOrEmpty(model.AboutMe))
            {
                try
                {
                    currentUser.AboutMe = model.AboutMe;
                    await _userService.Update(currentUser);
                    await _userManager.UpdateAsync(currentUser);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = ex.Message });
                }
            }


            return Json(new { success = false, message = "User not found" });

        }

        [HttpPost]

        public async Task<IActionResult> HidePost(int id)
        {
            var post = await _postService.GetPostById(id);

            if (post != null)
            {
                try
                {
                    post.Status = (post.Status == "public" ? "private" : "public");
                    await _postService.Update(post);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = ex.Message });
                }
            }


            return Json(new { success = false, message = "Post not found" });
        }

        [HttpPost]

        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postService.GetPostById(id);

            if (post != null)
            {
                try
                {
                    await _postService.Remove(post);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return Json(new { success = false, message = ex.Message });
                }
            }


            return Json(new { success = false, message = "Post not found" });
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _postService.GetPostById(id);

            if (post != null)
            {
                return Json(new { success = true, data = new { message = post.Description, imageUrl = post.ImageUrl, videoUrl = post.VideoUrl } });
            }

            return Json(new { success = false, message = "Post not found" });
        }



        [HttpPost]
        public async Task<IActionResult> UpdatePost(PostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { success = false, message = "Invalid data." });
            }

            var post = await _postService.GetPostById(model.Id);
            if (post == null)
            {
                return NotFound(new { success = false, message = "Post not found." });
            }

            post.Description = model.Description;
            post.ImageUrl = model.ImageUrl;
            post.VideoUrl = model.VideoUrl;


            await _postService.Update(post);

            // Güncellenen postun HTML içeriğini döndür
            return PartialView("_PostPartial", new PostPartialViewModel { Post = post });
        }



    }
}
