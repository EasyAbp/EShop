using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetailHistories.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetailHistories
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product-detail-history")]
    public class ProductDetailHistoryController : ProductsController, IProductDetailHistoryAppService
    {
        private readonly IProductDetailHistoryAppService _service;

        public ProductDetailHistoryController(IProductDetailHistoryAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDetailHistoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductDetailHistoryDto>> GetListAsync(GetProductDetailHistoryListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("by-time/{productId}")]
        public Task<ProductDetailHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            return _service.GetByTimeAsync(productId, modificationTime);
        }
    }
}