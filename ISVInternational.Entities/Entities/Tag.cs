using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{
    public class Tag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public int PostId { get; set; }
        public int IsDeleted { get; set; }
        public virtual BlogPost Post { get; set; }

    }
}
