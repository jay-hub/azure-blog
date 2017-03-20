using System;

namespace ISVInternational.Entities.DTO
{
    public class LikeDTO
    {
        public int LikeId { get; set; }
        public int LikeType { get; set; }
        public Guid LikedBy { get; set; }
        public DateTime LikedOn { get; set; }
        public int PostId { get; set; }
        public string LikedOnsDisplay => LikedOn.ToString("F");
        public string LikedByDisplay { get; set; }
    }
}