using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ThirdDelivery.Models;
using ThirdDelivery.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ThirdDelivery.ViewModels;

namespace ThirdDelivery.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public ProfileController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var posts = (await _postService.GetAllPostsAsync())
                .Where(p => p.UserName == userName)
                .ToList();

            var model = new ProfileViewModel
            {
                CurrentUser = await _userManager.FindByNameAsync(userName),
                Posts = posts
            };

            return View(model);
        }

       
        [HttpPost]
        public async Task<IActionResult> CreatePost(string Content)
        {
            if (!string.IsNullOrEmpty(Content))
            {
                var userId = _userManager.GetUserId(User); // ✅ ia userId-ul
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    var post = new Post
                    {
                        Title = "New Profile Post",
                        Content = Content,
                        CreatedAt = DateTime.Now,
                        UserName = user.UserName,
                        ApplicationUserId = user.Id // <- AICI E ESENȚIAL
                    };

                    await _postService.CreatePostAsync(post, userId);
                }
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> LikePost(int id)
        {
            await _postService.LikePostAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int postId, string content, string userId)
        {
            await _postService.AddCommentAsync(postId, content, userId);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profileImage)
        {
            if (profileImage != null && profileImage.Length > 0)
            {
                var userId = _userManager.GetUserId(User);
                var user = await _userManager.FindByIdAsync(userId);

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // 🔧 creează dacă lipsește

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + profileImage.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(stream);
                }

                user.ProfilePictureUrl = "/images/" + uniqueFileName;
                await _userManager.UpdateAsync(user);
            }

            return RedirectToAction("Index");
        }

    }
}
