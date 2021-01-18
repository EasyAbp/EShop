using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product")]
    public class ProductController : ProductsController, IProductAppService
    {
        private readonly IProductAppService _service;

        public ProductController(IProductAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListInput input)
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

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpPost]
        [Route("{id}/sku")]
        public Task<ProductDto> CreateSkuAsync(Guid id, CreateProductSkuDto input)
        {
            return _service.CreateSkuAsync(id, input);
        }

        [HttpPut]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> UpdateSkuAsync(Guid id, Guid productSkuId, UpdateProductSkuDto input)
        {
            return _service.UpdateSkuAsync(id, productSkuId, input);
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("by-code/{code}")]
        public Task<ProductDto> GetByCodeAsync(Guid storeId, string code)
        {
            return _service.GetByCodeAsync(storeId, code);
        }

        [HttpDelete]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> DeleteSkuAsync(Guid id, Guid productSkuId)
        {
            return _service.DeleteSkuAsync(id, productSkuId);
        }

        [HttpGet]
        [Route("product-group")]
        public Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            return _service.GetProductGroupListAsync();
        }
    }
}