namespace ThirdDelivery.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        // 👇 Adăugăm MutualFriends
        public int MutualFriends { get; set; }
        public string ImagePath { get; set; } = string.Empty; // 🔥 Adaugă asta!
    }
}
