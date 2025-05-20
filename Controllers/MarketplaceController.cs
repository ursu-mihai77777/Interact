using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class MarketplaceController : Controller
    {
        private readonly IMarketplaceService _marketplaceService;
        private readonly UserManager<User> _userManager;

        public MarketplaceController(IMarketplaceService marketplaceService, UserManager<User> userManager)
        {
            _marketplaceService = marketplaceService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "/images/profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            var items = await _marketplaceService.GetAllItemsAsync();
            return View(items);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "/images/profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MarketplaceItem model, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", await _marketplaceService.GetAllItemsAsync());
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                model.ImageUrl = "/images/" + uniqueFileName;
            }

            model.CreatedAt = DateTime.Now;
            model.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _marketplaceService.CreateItemAsync(model);
            return RedirectToAction("Index");
        }
    }
}
