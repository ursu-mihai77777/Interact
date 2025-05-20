using Microsoft.AspNetCore.Mvc;
using ThirdDelivery.Models;

namespace ThirdDelivery.ViewModels
{
    public class ProfileViewModel
    {
        public User CurrentUser { get; set; }
        public List<Post> Posts { get; set; }
    }
}
