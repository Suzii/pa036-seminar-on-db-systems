using DataAccess.Model;
using System.Data.Entity;

namespace DataAccess.Context
{
    public abstract class AppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
