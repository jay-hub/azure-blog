using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{

    public enum LikeType
    {
        Like = 1,
        Wow = 2, 
        Sad = 3,
        Wtf = 4,
        Angry = 5
    }

    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LikeId { get; set; }
        public int LikeType { get; set; }
        public Guid LikedBy { get; set; }
        public DateTime LikedOn { get; set; }
        public int PostId { get; set; }
        public virtual User User { get; set; }
        public virtual BlogPost Post { get; set; }
    }
}
