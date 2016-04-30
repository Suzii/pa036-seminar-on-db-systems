using DataAccess.Model;
using Shared.Filters;

namespace DataAccess.Data
{
    public interface IStores : ICrud<Store, StoreFilter>
    {
    }
}
