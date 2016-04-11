using System.Data.Entity;

namespace DataAccess.Context
{
    public class AppContext2 : AppContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
