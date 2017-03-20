using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{

    public enum ImageType
    {
        ProfileImage = 1,
        BlogPost = 2
    }

    public class ProfileImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
        public int ImageType { get; set; }
        public int IsDeleted { get; set; }

    }
}
