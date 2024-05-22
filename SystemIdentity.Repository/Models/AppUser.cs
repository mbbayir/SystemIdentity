using Microsoft.AspNetCore.Identity;

namespace SystemIdentity.Repository.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string City { get; set; }

        public List<AppRole> Roles {get; set; }
    }
}