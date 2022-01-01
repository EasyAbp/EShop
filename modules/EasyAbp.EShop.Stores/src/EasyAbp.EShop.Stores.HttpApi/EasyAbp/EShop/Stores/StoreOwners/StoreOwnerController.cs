using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    [RemoteService(Name = EShopStoresRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/stores/store-owner")]
    public class StoreOwnerController : StoresController
    {
        private readonly IStoreOwnerAppService _service;

        public StoreOwnerController(IStoreOwnerAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<StoreOwnerDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<StoreOwnerDto>> GetListAsync(GetStoreOwnerListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<StoreOwnerDto> CreateAsync(CreateUpdateStoreOwnerDto input)
        {
            return _service.CreateAsync(input);
        }
        
        [HttpPut]
        [Route("{id}")]
        public Task<StoreOwnerDto> UpdateAsync(Guid id, CreateUpdateStoreOwnerDto input)
        {
            return _service.UpdateAsync(id, input);
        }
        
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }
    }
}