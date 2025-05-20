    using System.ComponentModel.DataAnnotations;

    namespace ThirdDelivery.Models
    {
        public class MarketplaceItem
        {
            public int Id { get; set; }

            
            public string Title { get; set; }

            public string Description { get; set; }

          
            public decimal Price { get; set; }

            public string Location { get; set; }

            public string? ImageUrl { get; set; }

            public string? ApplicationUserId { get; set; }

            public User? ApplicationUser { get; set; }

            public DateTime CreatedAt { get; set; } = DateTime.Now;
        }
    }

