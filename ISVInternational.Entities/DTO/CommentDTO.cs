using System;

namespace ISVInternational.Entities.DTO
{
    public class CommentDTO
    {

        public int Id { get; set; }
        public string CommentTitle { get; set; }
        public string CommentContent { get; set; }
        public int IsDeleted { get; set; }
        public int IsReported { get; set; }
        public int PostId { get; set; }
        public DateTime PostedDate { get; set; }
        public string PostedDateDisplay => PostedDate.ToString("F");
        public Guid PostedUser { get; set; }
        public string PostedUserDisplay { get; set; }

    }
}