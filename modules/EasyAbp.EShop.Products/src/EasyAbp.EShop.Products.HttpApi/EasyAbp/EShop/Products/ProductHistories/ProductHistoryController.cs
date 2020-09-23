using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductHistories.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductHistories
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/eShop/products/productHistory")]
    public class ProductHistoryController : ProductsController, IProductHistoryAppService
    {
        private readonly IProductHistoryAppService _service;

        public ProductHistoryController(IProductHistoryAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductHistoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductHistoryDto>> GetListAsync(GetProductHistoryListDto input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("byTime/{productId}")]
        public Task<ProductHistoryDto> GetByTimeAsync(Guid productId, DateTime modificationTime)
        {
            return _service.GetByTimeAsync(productId, modificationTime);
        }
    }
}