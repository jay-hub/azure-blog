using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{
    public class Comment
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }
        public int IsDeleted { get; set; }
        public int IsReported { get; set; }
        public DateTime PostedDate { get; set; }
        public Guid PostedUser { get; set; }
        public virtual User User { get; set; }
        public virtual BlogPost Post { get; set; }
        
    }
}
