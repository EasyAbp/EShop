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

        [HttpGet]
        [Route("{id}/abandoned")]
        [RemoteService(false)]
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

        [HttpDelete]
        [Route("{id}/abandoned")]
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
        [Route("{id}/sku")]
        public Task<ProductDto> CreateSkuAsync(Guid id, Guid storeId, CreateProductSkuDto input)
        {
            return _service.CreateSkuAsync(id, storeId, input);
        }

        [HttpPut]
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> UpdateSkuAsync(Guid id, Guid productSkuId, Guid storeId, UpdateProductSkuDto input)
        {
            return _service.UpdateSkuAsync(id, productSkuId, storeId, input);
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
        [Route("{id}/sku/{productSkuId}")]
        public Task<ProductDto> DeleteSkuAsync(Guid id, Guid productSkuId, Guid storeId)
        {
            return _service.DeleteSkuAsync(id, productSkuId, storeId);
        }

        [HttpGet]
        [Route("productGroup")]
        public Task<ListResultDto<ProductGroupDto>> GetProductGroupListAsync()
        {
            return _service.GetProductGroupListAsync();
        }
    }
}