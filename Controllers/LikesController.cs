using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThirdDelivery.Models;
using ThirdDelivery.Services;

namespace ThirdDelivery.Controllers
{
    public class LikeController : Controller
    {
        private readonly ILikeService _likeService;
        private readonly InteractDbContext _context;

        public LikeController(ILikeService likeService, InteractDbContext context)
        {
            _likeService = likeService;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int postId)
        {
            await _context.Likes.AddAsync(new Like { PostId = postId }); 
            return RedirectToAction("Index", "Home");
        }
    }
}
