using Microsoft.EntityFrameworkCore;

namespace BBS_POS.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options):base(options) { }

        public DbSet<User> tbl_user { get; set; }
        public DbSet<Product> tbl_product { get; set; }
        public DbSet<Sale> tbl_sale { get; set; }
        public DbSet<Quantity> tbl_quantity { get; set; }
    }
}
