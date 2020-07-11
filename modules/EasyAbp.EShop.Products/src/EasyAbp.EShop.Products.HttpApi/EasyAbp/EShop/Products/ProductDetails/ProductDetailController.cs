using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetails
{
    [RemoteService(Name = "EShopProducts")]
    [Route("/api/eShop/products/productDetail")]
    public class ProductDetailController : ProductsController, IProductDetailAppService
    {
        private readonly IProductDetailAppService _service;

        public ProductDetailController(IProductDetailAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductDetailDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductDetailDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpPost]
        public Task<ProductDetailDto> CreateAsync(CreateUpdateProductDetailDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<ProductDetailDto> UpdateAsync(Guid id, CreateUpdateProductDetailDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpDelete]
        [Route("{id}")]
        [RemoteService(false)]
        public Task DeleteAsync(Guid id, Guid storeId)
        {
            return _service.DeleteAsync(id, storeId);
        }
    }
}