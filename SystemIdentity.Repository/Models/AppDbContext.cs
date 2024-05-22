using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SystemIdentity.Repository.Models;

namespace SystemIdentity.Repository.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, String>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
        
        }


    }
}
