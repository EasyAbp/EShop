using System;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    [RemoteService(Name = EShopPluginsBookingRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/plugins/booking/product-asset-category")]
    public class ProductAssetCategoryController : BookingController, IProductAssetCategoryAppService
    {
        private readonly IProductAssetCategoryAppService _service;

        public ProductAssetCategoryController(IProductAssetCategoryAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("period/{productAssetCategoryId}")]
        public virtual Task<ProductAssetCategoryDto> CreatePeriodAsync(Guid productAssetCategoryId, CreateProductAssetCategoryPeriodDto input)
        {
            return _service.CreatePeriodAsync(productAssetCategoryId, input);
        }

        [HttpPut]
        [Route("period")]
        public virtual Task<ProductAssetCategoryDto> UpdatePeriodAsync(Guid productAssetCategoryId, Guid periodId, UpdateProductAssetCategoryPeriodDto input)
        {
            return _service.UpdatePeriodAsync(productAssetCategoryId, periodId, input);
        }

        [HttpDelete]
        [Route("period")]
        public virtual Task<ProductAssetCategoryDto> DeletePeriodAsync(Guid productAssetCategoryId, Guid periodId)
        {
            return _service.DeletePeriodAsync(productAssetCategoryId, periodId);
        }

        [HttpPost]
        [Route("")]
        public virtual Task<ProductAssetCategoryDto> CreateAsync(CreateProductAssetCategoryDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProductAssetCategoryDto> UpdateAsync(Guid id, UpdateProductAssetCategoryDto input)
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
        public virtual Task<ProductAssetCategoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<ProductAssetCategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}