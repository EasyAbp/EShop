using System;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    [RemoteService(Name = EShopPluginsBookingRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/e-shop/plugins/booking/product-asset")]
    public class ProductAssetController : BookingController, IProductAssetAppService
    {
        private readonly IProductAssetAppService _service;

        public ProductAssetController(IProductAssetAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("period/{productAssetId}")]
        public virtual Task<ProductAssetDto> CreatePeriodAsync(Guid productAssetId, CreateProductAssetPeriodDto input)
        {
            return _service.CreatePeriodAsync(productAssetId, input);
        }

        [HttpPut]
        [Route("period")]
        public virtual Task<ProductAssetDto> UpdatePeriodAsync(Guid productAssetId, Guid periodId, UpdateProductAssetPeriodDto input)
        {
            return _service.UpdatePeriodAsync(productAssetId, periodId, input);
        }

        [HttpDelete]
        [Route("period")]
        public virtual Task<ProductAssetDto> DeletePeriodAsync(Guid productAssetId, Guid periodId)
        {
            return _service.DeletePeriodAsync(productAssetId, periodId);
        }

        [HttpPost]
        [Route("")]
        public virtual Task<ProductAssetDto> CreateAsync(CreateProductAssetDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<ProductAssetDto> UpdateAsync(Guid id, UpdateProductAssetDto input)
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
        public virtual Task<ProductAssetDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<ProductAssetDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}