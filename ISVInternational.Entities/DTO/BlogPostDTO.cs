using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISVInternational.Entities.Entities;

namespace ISVInternational.Entities.DTO
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string Post { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateDisplay => CreatedDate.ToString("F");
        public Guid UserId { get; set; }
        public String UserDisplay { get; set; }
        public int IsPosted { get; set; }
        public int IsArchived { get; set; }
        public int PrivacyType { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<CommentDTO> Comments { get; set; }
        public ICollection<LikeDTO> Likes { get; set; }
    }
}
