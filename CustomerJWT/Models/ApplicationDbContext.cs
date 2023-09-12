using Bogus;
using Microsoft.EntityFrameworkCore;

namespace CustomerJWT.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var ids = 1;
            var stock = new Faker<Customer>()
                .RuleFor(m => m.Id, f => ids++)
                .RuleFor(u => u.Name, (f) => f.Name.FullName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email());
              


            modelBuilder
                .Entity<Customer>()
                .HasData(stock.GenerateBetween(500, 500));
        }
    }
}
