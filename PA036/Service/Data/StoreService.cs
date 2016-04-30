using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;

namespace Service.Data
{
    public class StoreService: BaseService<StoreDTO, Store, StoreFilter>, IStoreService
    {
        public sealed override DbSettings DbSettings { get; set; }
        protected sealed override ICrud<Store, StoreFilter> DalInstance { get; set; }

        public StoreService(DbSettings dbSettings)
        {
            DalInstance = new Stores();
            DbSettings = dbSettings;
        }
        
        protected override Store ToEntity(StoreDTO dto)
        {
            if (dto == null)
            {
                return null;
            }

            var result = new Store()
            {
                Id = dto.Id,
                Name = dto.Name,
                City = dto.City,
                State = dto.State
            };

            return result;
        }

        protected override StoreDTO ToDto(Store entity)
        {
            if (entity == null)
            {
                return null;
            }

            var result = new StoreDTO()
            {
                Id = entity.Id,
                Name = entity.Name,
                City = entity.City,
                State = entity.State
            };

            return result;
        }
    }
}