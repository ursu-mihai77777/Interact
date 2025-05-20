using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ThirdDelivery.Models
{
    public class Like
    {
        public int LikeId { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;


    }

}
