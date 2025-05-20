namespace ThirdDelivery.Models
{
    public class FriendSuggestion
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int MutualFriends { get; set; }
        public string ImagePath { get; set; } = string.Empty; // 🔥 Adaugă această linie
        public string ApplicationUserId { get; set; } // ✅ ADĂUGĂ ASTA
    }
}
