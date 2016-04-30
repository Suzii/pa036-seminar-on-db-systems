using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using DataAccess.Model;
using Service.DTO;
using Shared.Filters;
using Shared.Settings;

namespace Service.Data
{
    public abstract class BaseService<TDto, TDataModel, TFilter>
        : IService<TDto, TFilter>
        where TDto : class, IDTO, new()
        where TDataModel : class, IDataModel, new()
        where TFilter : BaseFilter, new()
    {
        public abstract DbSettings DbSettings { get; set; }

        protected abstract ICrud<TDataModel, TFilter> DalInstance { get; set; }

        protected abstract TDataModel ToEntity(TDto dto);

        protected abstract TDto ToDto(TDataModel entity);
        

        public async Task<TDto> GetAsync(int id)
        {
            return ToDto(await DalInstance.GetAsync(id, DbSettings));
        }

        public TDto Get(int id)
        {
            return ToDto(DalInstance.Get(id, DbSettings));
        }

        public async Task<IList<TDto>> GetAllAsync()
        {
            return await GetAsync(null);
        }

        public IList<TDto> GetAll()
        {
            return Get(null);
        }

        public async Task<IList<TDto>> GetAsync(TFilter filter)
        {
            return (await DalInstance.GetAsync(filter, DbSettings)).Select(ToDto).ToList();
        }

        public IList<TDto> Get(TFilter filter)
        {
            return DalInstance.Get(filter, DbSettings).Select(ToDto).ToList();
        }

        // TODO ?? should return newly created entity?
        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await DalInstance.CreateAsync(entity, DbSettings);
            return null;
        }

        // TODO ?? should return newly created entity?
        public TDto Create(TDto dto)
        {
            var entity = ToEntity(dto);
            DalInstance.Create(entity, DbSettings);
            return null;
        }

        // TODO ?? should return updated entity?
        public async Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = ToEntity(dto);
            await DalInstance.UpdateAsync(entity, DbSettings);
            return null;
        }

        // TODO ?? should return updated entity?
        public TDto Update(TDto dto)
        {
            var entity = ToEntity(dto);
            DalInstance.Update(entity, DbSettings);
            return null;
        }

        public async Task DeleteAsync(int id)
        {
            await DalInstance.DeleteAsync(id, DbSettings);
        }

        public void Delete(int id)
        {
            DalInstance.Delete(id, DbSettings);
        }

        public async Task<int> TotalCountAsync()
        {
            return await DalInstance.CountAsync(DbSettings);
        }

        public int TotalCount()
        {
            return DalInstance.Count(DbSettings);
        }
    }
}