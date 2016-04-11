using DataAccess.Context;
using DataAccess.LinqExtension;
using DataAccess.Model;
using Shared.Filters;
using Shared.Settings;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Data
{
    public abstract class BaseData
    {
        public IQueryable<T> ApplyBaseModifiers<T>(IQueryable<T> query, BaseFilter filter) where T : IDataModel
        {
            if (filter.Ids != null && filter.Ids.Any())
                query = query.Where(x => filter.Ids.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(filter.OrderProperty))
                query = query.OrderByField(filter.OrderProperty, filter.OrderDesc);

            if (filter.Skip > 0)
                query = query.Skip(filter.Skip.Value);

            if (filter.Take > 0)
                query = query.Take(filter.Take.Value);

            return query;
        }

        protected AppContext CreateAppContext(DbSettings dbSettings)
        {
            if (dbSettings == null)
                dbSettings = new DbSettings();

            return dbSettings.UseSecondAppContext ? (AppContext)new AppContext2() : new AppContext1();
        }
    }
}
