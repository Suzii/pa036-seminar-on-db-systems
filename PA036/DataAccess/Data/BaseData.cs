using DataAccess.LinqExtension;
using DataAccess.Model;
using Shared.Modifiers;
using System.Linq;

namespace DataAccess.Data
{
    public abstract class BaseData
    {
        public static IQueryable<T> ApplyBaseModifiers<T>(IQueryable<T> query, BaseModifier modifier) where T : IDataModel
        {
            if (modifier.Ids != null && modifier.Ids.Any())
                query = query.Where(x => modifier.Ids.Contains(x.Id));

            if (!string.IsNullOrWhiteSpace(modifier.OrderProperty))
                query = query.OrderByField(modifier.OrderProperty, modifier.OrderDesc);

            if (modifier.Skip.HasValue && modifier.Skip.Value > 0)
                query = query.Skip(modifier.Skip.Value);

            if (modifier.Take.HasValue && modifier.Take.Value > 0)
                query = query.Take(modifier.Take.Value);

            return query;
        }
    }
}