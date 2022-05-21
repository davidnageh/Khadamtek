using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _mvcproject_.Models
{
    public class OurContext : IdentityDbContext<ApplicationUser>
    {
        public OurContext(DbContextOptions<OurContext> options) : base(options)
        {

        }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<ProviderWorks> ProviderWorks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            builder.Entity<ProviderWorks>().HasKey(e => new { e.Provider_Id, e.ImgPath } );
            base.OnModelCreating(builder);
        }
    }
}
