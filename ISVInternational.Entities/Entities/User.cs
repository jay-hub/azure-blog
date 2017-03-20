using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISVInternational.Entities.Entities
{

    public enum UserType
    {
        Admin = 1,
        User = 2,
        Guest = 3
    }

    public class User
    {
        [Key]
        public Guid UserOId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public string ProfileUrl { get; set; }
        public int IsDeleted { get; set; }
        public int UserType { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLoginTime { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public virtual ProfileImage ProfileImages { get; set; }
        public virtual Organization Orgnanization { get; set; }
        public virtual ICollection<BlogPost> BlogPostses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
