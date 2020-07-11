using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores
{
    [RemoteService(Name = "EShopStores")]
    [Route("/api/eShop/stores/store")]
    public class StoreController : StoresController, IStoreAppService
    {
        private readonly IStoreAppService _service;

        public StoreController(IStoreAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<StoreDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<StoreDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<StoreDto> CreateAsync(CreateUpdateStoreDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<StoreDto> UpdateAsync(Guid id, CreateUpdateStoreDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("default")]
        public Task<StoreDto> GetDefaultAsync()
        {
            return _service.GetDefaultAsync();
        }
    }
}