using System.Data.Entity;

namespace DataAccess.Context
{
    public class AppContext1 : AppContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
