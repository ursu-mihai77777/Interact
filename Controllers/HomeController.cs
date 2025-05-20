using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThirdDelivery.Models;
using ThirdDelivery.Services;
using System.Security.Claims;


namespace ThirdDelivery.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ILikeService _likeService;
        private readonly ICommentService _commentService;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;    

        public HomeController(IPostService postService, ILikeService likeService, ICommentService commentService, ILogger<HomeController> logger, UserManager<User> userManager)
        {
            _postService = postService;
            _likeService = likeService;
            _commentService = commentService;
            _logger = logger;
            _userManager = userManager;
        }


        // Afișează pagina principală cu toate postările
        public async Task<IActionResult> Index(string filter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            var posts = await _postService.GetAllPostsAsync();

            if (!string.IsNullOrEmpty(filter))
            {
                posts = posts
                    .Where(p => (p.Content?.Contains(filter, StringComparison.OrdinalIgnoreCase) ?? false) ||
                                (p.Title?.Contains(filter, StringComparison.OrdinalIgnoreCase) ?? false))
                    .ToList();
                ViewBag.Filter = filter;
            }

            return View(posts);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string content, IFormFile? ImageFile, string? videoUrl,IFormFile? videoFile)
        {
            if (string.IsNullOrWhiteSpace(content) && ImageFile == null && string.IsNullOrWhiteSpace(videoUrl))
            {
                TempData["Error"] = "Postarea trebuie să conțină conținut, o imagine sau un video.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Trebuie să fii logat.";
                return RedirectToAction("Index");
            }

            string? imagePath = null;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                Directory.CreateDirectory(uploadsFolder); // Creează dacă nu există
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                imagePath = "/images/" + uniqueFileName;
            }
            string? videoPath = null;

            if (videoFile != null && videoFile.Length > 0)
            {
                var videoFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos");
                Directory.CreateDirectory(videoFolder); // creează folderul dacă nu există

                var uniqueVideoName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(videoFile.FileName);
                var videoFilePath = Path.Combine(videoFolder, uniqueVideoName);

                using (var stream = new FileStream(videoFilePath, FileMode.Create))
                {
                    await videoFile.CopyToAsync(stream);
                }

                videoPath = "/videos/" + uniqueVideoName;
            }

            var post = new Post
            {
                Title = "Postare nouă",
                Content = content,
                ImageUrl = imagePath,
                VideoUrl = videoPath, // 👈 aici
                CreatedAt = DateTime.Now,
                ApplicationUserId = user.Id,
                UserName = user.UserName
            };

            await _postService.CreatePostAsync(post, user.Id);

            TempData["Message"] = "✅ Postare creată!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> LikePost(int id)
        {
            await _likeService.AddLikeAsync(id); 
            return RedirectToAction(nameof(Index));
        }
        // POST: Comment on a post
        [HttpPost]
       [HttpPost]
public async Task<IActionResult> AddComment(int postId, string content)
{
            var userId = _userManager.GetUserId(User);
            await _commentService.AddCommentAsync(postId, content,userId);
          return RedirectToAction(nameof(Index));
}
       
    }
}
