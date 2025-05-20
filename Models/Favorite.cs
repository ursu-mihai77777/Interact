namespace ThirdDelivery.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostId { get; set; }  // Changed to int
        public int UserId { get; set; }  // Changed to int
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
