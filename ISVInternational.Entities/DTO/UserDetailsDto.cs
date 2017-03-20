using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ISVInternational.Entities.DTO
{
    public class UserDetailsDto
    {
        public UserDetailsDto()
        {
            Files = new List<HttpPostedFileBase>();
        }

        public String UserId { get; set; }
        public Guid UserGuid { get; set; }
        public string DisplayName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }
        public bool IsNewUser { get; set; }
        public string IdentityProvider { get; set; }
        public string AuthTime { get; set; }
        public string ProfileUrl { get; set; }
        public string Bio { get; set; }
        public List<HttpPostedFileBase> Files { get; set; }
    }
}
