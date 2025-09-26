using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecilStore.Business.Abstract;
using SecilStore.Common.Constants.HttpRequestUrls;
using SecilStore.Common.DTOs;
using SecilStore.Common.Results;
using SecilStore.Common.Validations.Abstract;
using SecilStore.Data.Model;

namespace SecilStore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigItemController : ControllerBase
    {
        private readonly IConfigItemService<ConfigurationItem> _configItemRepository;
        private readonly IModelStateResponseService _modelStateResponseService;
        public ConfigItemController(IConfigItemService<ConfigurationItem> configItemRepository, IModelStateResponseService modelStateResponseService)
        {
            _configItemRepository = configItemRepository;
            _modelStateResponseService = modelStateResponseService;
        }

        [HttpPost]
        [Route($"~{HttpConfigItemRequestUrl.CreateConfigItemUrl}")]
        public async Task<IDataResult<ConfigurationItemDto?>> Create([FromBody] ConfigurationItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<ConfigurationItemDto?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _configItemRepository.Create(dto);
        }

        [HttpGet]
        [Route($"~{HttpConfigItemRequestUrl.GetConfigItemByApplicationNameUrl}")]
        public async Task<IDataResult<ConfigurationItemData?>> GetActiveConfigsByAppAsync(string applicationName)
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<ConfigurationItemData?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _configItemRepository.GetActiveConfigsByAppAsync(applicationName);
        }

        [HttpGet]
        [Route($"~{HttpConfigItemRequestUrl.GetAllActivesUrl}")]
        public async Task<IDataResult<ConfigurationItemData?>> GetAllActives()
        {
            if (!ModelState.IsValid)
            {
                return new DataResult<ConfigurationItemData?>(_modelStateResponseService.HandleErrorMessage(ModelState));
            }
            return await _configItemRepository.GetAllActives();
        }
    }
}
