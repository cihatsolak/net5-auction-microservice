using ESourcing.UI.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ESourcing.UI.Infrastructure.Data
{
    public class WebAppContext : IdentityDbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> dbContextOptionsBuilder) 
            : base(dbContextOptionsBuilder)
        {
        }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
