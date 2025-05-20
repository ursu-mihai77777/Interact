using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly UserManager<User> _userManager;

        public PostsController(IPostService postService, UserManager<User> userManager)
        {
            _postService = postService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();
            return View(posts);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content,ImageUrl,VideoUrl")] Post post)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Trebuie să fii logat pentru a crea o postare.";
                return RedirectToAction("Login", "Account");
            }

            // 🔑 Setează câmpurile NECESARE înainte de validare
            post.ApplicationUserId = user.Id;
            post.UserName = user.UserName;
            post.CreatedAt = DateTime.Now;

            // ✅ Abia acum validăm
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Modelul nu este valid.";
                return View(post);
            }

            await _postService.CreatePostAsync(post, user.Id);
            TempData["Message"] = "✅ Postarea a fost creată cu succes!";
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (id != post.PostId) return NotFound();

            if (ModelState.IsValid)
            {
                await _postService.UpdatePostAsync(post);
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }
        [Authorize(Roles = "Admin@gmail.com")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postService.DeletePostAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
