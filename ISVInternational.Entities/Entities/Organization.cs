using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISVInternational.Entities.Entities
{
    public class Organization
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrgId { get; set; }
        public string OrganizationId { get; set; }
        public string IsActive { get; set; }
        public int BillingProfile { get; set; }

    }
}
