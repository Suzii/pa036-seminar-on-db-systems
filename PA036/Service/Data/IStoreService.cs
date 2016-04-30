using Service.DTO;
using Shared.Filters;

namespace Service.Data
{
    public interface IStoreService: IService<StoreDTO, StoreFilter>
    {
    }
}

