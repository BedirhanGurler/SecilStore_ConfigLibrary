using SecilStore.Common.DTOs;
using SecilStore.Common.Results;
using SecilStore.Data.Abstract;
using SecilStore.Data.Model;

namespace SecilStore.Business.Abstract
{
    public interface IConfigItemService<TEntity> : IBaseDal<TEntity, Guid> where TEntity : ConfigurationItem
    {
        Task<IDataResult<ConfigurationItemDto?>> Create(ConfigurationItemDto dto);
        Task<IDataResult<ConfigurationItemDto?>> Delete(ConfigurationItemDto dto);
        Task<IDataResult<ConfigurationItemData?>> GetAllActives();
        Task<IDataResult<ConfigurationItemDto?>> GetById(Guid id);
        Task<IDataResult<ConfigurationItemData?>> GetActiveConfigsByAppAsync(string applicationName);
    }
}
