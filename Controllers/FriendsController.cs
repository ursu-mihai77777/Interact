using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendSuggestionService _friendSuggestionService;
        private readonly IFriendService _friendService;
        private readonly UserManager<User> _userManager;


        public FriendsController(IFriendSuggestionService friendSuggestionService, IFriendService friendService, UserManager<User> usermManager )
        {
            _friendSuggestionService = friendSuggestionService;
            _friendService = friendService;
            _userManager = usermManager;
        }

        // Afișează lista de sugestii
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            ViewBag.ProfilePictureUrl = user?.ProfilePictureUrl ?? "/images/profile.jpg";
            ViewBag.UserName = user?.UserName ?? "User";

            var suggestions = await _friendService.GetSuggestionsAsync(userId); // exemplu
            return View(suggestions);
        }

        // Adaugă un prieten nou din sugestii
        [HttpPost]
        public async Task<IActionResult> AddFriend(int id)
        {
            var suggestion = await _friendSuggestionService.GetSuggestionByIdAsync(id);
            if (suggestion != null)
            {
                var friend = new Friend
                {
                    Name = suggestion.Name,
                    MutualFriends = suggestion.MutualFriends,
                    ImagePath = suggestion.ImagePath
                };
                await _friendService.AddFriendAsync(friend);
                await _friendSuggestionService.RemoveSuggestionAsync(id);
                return Ok(); // ➔ NU RedirectToAction, ci Ok()
            }
            return NotFound();
        }
        // Șterge o sugestie (Remove Friend)
        [HttpPost]
        public async Task<IActionResult> RemoveSuggestion(int id)
        {
            await _friendSuggestionService.RemoveSuggestionAsync(id);
            return Ok(); // ➔ La fel, răspunde cu OK
        }

    }
}
