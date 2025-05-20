using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class FriendSuggestionsController : Controller
    {
        private readonly IFriendSuggestionService _friendSuggestionService;

        public FriendSuggestionsController(IFriendSuggestionService friendSuggestionService)
        {
            _friendSuggestionService = friendSuggestionService;
        }

        // GET: FriendSuggestions
        public async Task<IActionResult> Index()
        {
            var suggestions = await _friendSuggestionService.GetAllSuggestionsAsync();
            return View(suggestions);
        }

        // GET: FriendSuggestions/Details/5
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Details(int id)
        {
            var suggestion = await _friendSuggestionService.GetSuggestionByIdAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return View(suggestion);
        }

        // GET: FriendSuggestions/Create
        [Authorize(Roles = "Admin@gmail.com")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FriendSuggestions/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ImagePath,MutualFriends")] FriendSuggestion suggestion)
        {
            if (ModelState.IsValid)
            {
                await _friendSuggestionService.AddSuggestionAsync(suggestion);
                return RedirectToAction(nameof(Index));
            }
            return View(suggestion);
        }

        // GET: FriendSuggestions/Edit/5
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Edit(int id)
        {
            var suggestion = await _friendSuggestionService.GetSuggestionByIdAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return View(suggestion);
        }

        // POST: FriendSuggestions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Edit(int id, [Bind("FriendSuggestionId,Name,ImagePath,MutualFriends")] FriendSuggestion suggestion)
        {
            if (id != suggestion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _friendSuggestionService.UpdateSuggestionAsync(suggestion);
                return RedirectToAction(nameof(Index));
            }
            return View(suggestion);
        }

        // GET: FriendSuggestions/Delete/5
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Delete(int id)
        {
            var suggestion = await _friendSuggestionService.GetSuggestionByIdAsync(id);
            if (suggestion == null)
            {
                return NotFound();
            }
            return View(suggestion);
        }

        // POST: FriendSuggestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _friendSuggestionService.RemoveSuggestionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
