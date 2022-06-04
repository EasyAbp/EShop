using System;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    [RemoteService(Name = "BookingStoreAssetCategory")]
    [Route("/api/booking/store-asset-category")]
    public class StoreAssetCategoryController : BookingController, IStoreAssetCategoryAppService
    {
        private readonly IStoreAssetCategoryAppService _service;

        public StoreAssetCategoryController(IStoreAssetCategoryAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<StoreAssetCategoryDto> CreateAsync(CreateUpdateStoreAssetCategoryDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<StoreAssetCategoryDto> UpdateAsync(Guid id, CreateUpdateStoreAssetCategoryDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<StoreAssetCategoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<StoreAssetCategoryDto>> GetListAsync(GetStoreAssetCategoryListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}