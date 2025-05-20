using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class GroupController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public GroupController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "/images/profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            var allPosts = await _postService.GetAllPostsAsync();
            var groupPosts = allPosts.Where(p => p.IsGroupPost).ToList();

            return View(groupPosts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string content, IFormFile? imageFile, IFormFile? videoFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Index");

            string? imagePath = null;
            string? videoPath = null;

            if (imageFile != null)
            {
                var imageName = Guid.NewGuid() + "_" + Path.GetFileName(imageFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);
                using var stream = new FileStream(path, FileMode.Create);
                await imageFile.CopyToAsync(stream);
                imagePath = "/images/" + imageName;
            }

            if (videoFile != null)
            {
                var videoName = Guid.NewGuid() + "_" + Path.GetFileName(videoFile.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", videoName);
                using var stream = new FileStream(path, FileMode.Create);
                await videoFile.CopyToAsync(stream);
                videoPath = "/videos/" + videoName;
            }

            var post = new Post
            {
                Title = "Postare în grup",
                Content = content,
                ImageUrl = imagePath,
                VideoUrl = videoPath,
                CreatedAt = DateTime.Now,
                ApplicationUserId = user.Id,
                UserName = user.UserName,
                IsGroupPost = true
            };

            await _postService.CreatePostAsync(post, user.Id);
            return RedirectToAction("Index");
        }
    }
}
