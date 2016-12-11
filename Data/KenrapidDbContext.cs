using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Kenrapid.CRM.Web.Domain;
using Kenrapid.CRM.Web.Infrastructure.Data;

namespace Kenrapid.CRM.Web.Data
{
    public class KenrapidDbContext : IdentityDbContext<ApplicationUser>
    {
        public KenrapidDbContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; }
        public DbSet<Risk> Risks { get; set; }        
        public DbSet<Issue> Issues { get; set; }
        public DbSet<LogAction> Logs { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Assignments).WithRequired(i => i.AssignedTo);

            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}