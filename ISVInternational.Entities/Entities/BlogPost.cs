using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{

    public enum PrivacyType
    {
        Private = 1,
        Public = 2,
        OnlyTargetGroup= 3
    }

    public class BlogPost
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string BlogTitle { get; set; }
        public string Post { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid UserId { get; set; }
        public int IsPosted { get; set; }
        public int IsArchived { get; set; }
        public int PrivacyType{ get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment>  Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
