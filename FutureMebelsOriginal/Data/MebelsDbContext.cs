using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FutureMebelsOriginal.Data
{
    public class MebelsDbContext : IdentityDbContext<Customer>
    {
        public MebelsDbContext(DbContextOptions<MebelsDbContext> options)
            : base(options)
        {

        }
        public DbSet<Articul> Articuls { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Type> Types { get; set; }
    }
}