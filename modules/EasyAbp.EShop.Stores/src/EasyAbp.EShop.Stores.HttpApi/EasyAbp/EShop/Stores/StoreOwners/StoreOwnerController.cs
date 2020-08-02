using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    [RemoteService(Name = "EShopStores")]
    [Route("/api/eShop/stores/storeOwner")]
    public class StoreOwnerController : StoresController
    {
        private readonly IStoreOwnerAppService _service;

        public StoreOwnerController(IStoreOwnerAppService service)
        {
            _service = service;
        }

        [NonAction]
        [RemoteService(false)]
        public Task<StoreOwnerDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<StoreOwnerDto>> GetListAsync(GetStoreOwnerListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}