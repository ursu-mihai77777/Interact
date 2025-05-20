using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class WatchController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public WatchController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        // GET: Watch
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "/images/profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            var posts = await _postService.GetAllPostsForWatchAsync();
            return View(posts);
        }


        // POST: Watch/CreatePostDirect
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreatePostDirect(string content, IFormFile videoFile)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || videoFile == null) return RedirectToAction("Index");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos");
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(videoFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await videoFile.CopyToAsync(stream);
            }

            var post = new Post
            {
                Title = "Video nou",
                Content = content,
                VideoUrl = "/videos/" + uniqueFileName,
                CreatedAt = DateTime.Now,
                ApplicationUserId = user.Id,
                UserName = user.UserName
            };

            await _postService.CreatePostAsync(post, user.Id);
            return RedirectToAction("Index");
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
            await _postService.AddCommentAsync(postId, content,userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
