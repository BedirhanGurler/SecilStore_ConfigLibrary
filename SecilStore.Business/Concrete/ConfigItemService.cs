using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SecilStore.Business.Abstract;
using SecilStore.Common.Constants.ResponseMessages;
using SecilStore.Common.DTOs;
using SecilStore.Common.Results;
using SecilStore.Data.Concrete;
using SecilStore.Data.Context;
using SecilStore.Data.Model;

namespace SecilStore.Business.Concrete
{
    public class ConfigItemService<TEntity> : BaseDal<TEntity, Guid>, IConfigItemService<TEntity> where TEntity : ConfigurationItem
    {
        public ConfigDbContext Context { get; }

        public async Task<IDataResult<ConfigurationItemDto?>> Create(ConfigurationItemDto dto)
        {
            try
            {
                var now = DateTimeOffset.Now;
                var item = new ConfigurationItem(Guid.NewGuid())
                {
                    Name = dto.Name,
                    Type = dto.Type,
                    Value = dto.Value,
                    IsActive = true,
                    ApplicationName = dto.ApplicationName,
                    CreatedDate = now,
                    ModifiedDate = now
                };
                var result = await InsertAsync((TEntity)item) as ConfigurationItem;
                if (result != null)
                {
                    return new DataResult<ConfigurationItemDto?>(ResultStatus.Success, ResponseMessages.Success, dto);
                }
                else
                {
                    return new DataResult<ConfigurationItemDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }
            catch (Exception ex)
            {
                return new DataResult<ConfigurationItemDto?>(ResultStatus.Error, ex.ToString(), null);
            }
        }

        public async Task<IDataResult<ConfigurationItemData?>> GetActiveConfigsByAppAsync(string applicationName)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var dataList = new List<ConfigurationItemDto>();
                var query = (from c in context.ConfigurationItem.AsNoTracking()
                             where c.ApplicationName == applicationName && c.IsActive == true
                             orderby c.CreatedDate descending
                             select new ConfigurationItemDto
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Type = c.Type,
                                 Value = c.Value,
                                 IsActive = c.IsActive,
                                 ApplicationName = c.ApplicationName,
                                 CreatedDate = c.CreatedDate,
                                 ModifiedDate = c.ModifiedDate
                             });
                try
                {
                    dataList = await query.ToListAsync();
                    var configData = new ConfigurationItemData();
                    configData.data = dataList;
                    configData.numberOfAllKeys = dataList.Count;

                    if (query != null && dataList.Count > 0)
                    {
                        return new DataResult<ConfigurationItemData?>(ResultStatus.Success, ResponseMessages.Success, configData);
                    }
                    else
                    {
                        return new DataResult<ConfigurationItemData?>(ResultStatus.Error, ResponseMessages.Error, null);
                    }
                }
                catch (Exception ex)
                {
                    return new DataResult<ConfigurationItemData?>(ResultStatus.Error, ex.ToString(), null);
                }
            }
        }

        public async Task<IDataResult<ConfigurationItemData?>> GetAllActives()
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var dataList = new List<ConfigurationItemDto>();
                var query = (from c in context.ConfigurationItem.AsNoTracking()
                             where c.IsActive == true
                             orderby c.CreatedDate descending
                             select new ConfigurationItemDto
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 Type = c.Type,
                                 Value = c.Value,
                                 IsActive = c.IsActive,
                                 ApplicationName = c.ApplicationName,
                                 CreatedDate = c.CreatedDate,
                                 ModifiedDate = c.ModifiedDate
                             });
                try
                {
                    dataList = await query.ToListAsync();
                    var configData = new ConfigurationItemData();
                    configData.data = dataList;
                    configData.numberOfAllKeys = dataList.Count;

                    if (query != null && dataList.Count > 0)
                    {
                        return new DataResult<ConfigurationItemData?>(ResultStatus.Success, ResponseMessages.Success, configData);
                    }
                    else
                    {
                        return new DataResult<ConfigurationItemData?>(ResultStatus.Error, ResponseMessages.Error, null);
                    }
                }
                catch (Exception ex)
                {
                    return new DataResult<ConfigurationItemData?>(ResultStatus.Error, ex.ToString(), null);
                }
            }
        }

        public async Task<IDataResult<ConfigurationItemDto?>> GetById(Guid id)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                var qData = await (from c in context.ConfigurationItem.AsNoTracking()
                                   where c.Id == id
                                   select new ConfigurationItemDto
                                   {
                                       Id = c.Id,
                                       Name = c.Name,
                                       ApplicationName = c.ApplicationName,
                                       Type = c.Type,
                                       Value = c.Value,
                                       IsActive = c.IsActive
                                   }).FirstOrDefaultAsync();
                if(qData != null)
                {
                    return new DataResult<ConfigurationItemDto?>(ResultStatus.Success, ResponseMessages.Success, qData);
                }
                else
                {
                    return new DataResult<ConfigurationItemDto?>(ResultStatus.Error, ResponseMessages.Error, null);
                }
            }
        }

        public async Task<IDataResult<ConfigurationItemDto?>> Delete(ConfigurationItemDto dto)
        {
            using (var context = new ConfigDbContextFactory().CreateDbContext())
            {
                try
                {
                    var now = DateTimeOffset.Now;

                    var item = await context.ConfigurationItem.FirstOrDefaultAsync(p => p.Id == dto.Id);
                    if (item != null)
                    {
                        item.IsActive = false;
                        item.ModifiedDate = now;
                        await context.SaveChangesAsync();
                        return new DataResult<ConfigurationItemDto?>(ResultStatus.Success, dto);
                    }
                    else
                    {
                        return new DataResult<ConfigurationItemDto?>(ResultStatus.NotFound, dto);
                    }
                }
                catch (Exception ex)
                {
                    return new DataResult<ConfigurationItemDto?>(ResultStatus.Error, ex.ToString(), dto);
                }
            }
        }
    }
}
