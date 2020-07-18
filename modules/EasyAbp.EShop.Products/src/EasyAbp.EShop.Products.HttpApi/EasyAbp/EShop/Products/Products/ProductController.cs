using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products
{
    [RemoteService(Name = "EShopProducts")]
    [Route("/api/eShop/products/product")]
    public class ProductController : ProductsController, IProductAppService
    {
        private readonly IProductAppService _service;

        public ProductController(IProductAppService service)
        {
            _service = service;
        }

        [Route("{id}")]
        [RemoteService(false)]
        [NonAction]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [NonAction]
        [RemoteService(false)]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id, Guid storeId)
        {
            return _service.DeleteAsync(id, storeId);
        }

        [HttpPost]
        [Route("sku")]
        public Task<ProductDto> CreateSkuAsync(Guid productId, Guid storeId, CreateProductSkuDto input)
        {
            return _service.CreateSkuAsync(productId, storeId, input);
        }

        [HttpPut]
        [Route("sku")]
        public Task<ProductDto> UpdateSkuAsync(Guid productId, Guid productSkuId, Guid storeId, UpdateProductSkuDto input)
        {
            return _service.UpdateSkuAsync(productId, productSkuId, storeId, input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id, Guid storeId)
        {
            return _service.GetAsync(id, storeId);
        }

        [HttpGet]
        [Route("byCode/{storeId}")]
        public Task<ProductDto> GetByCodeAsync(string code, Guid storeId)
        {
            return _service.GetByCodeAsync(code, storeId);
        }

        [HttpDelete]
        [Route("sku")]
        public Task<ProductDto> DeleteSkuAsync(Guid productId, Guid productSkuId, Guid storeId)
        {
            return _service.DeleteSkuAsync(productId, productSkuId, storeId);
        }
    }
}