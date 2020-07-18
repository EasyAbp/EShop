using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductTags.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductTags
{
    [RemoteService(Name = "ProductTagService")]
    [Route("/api/products/productTag")]
    public class ProductTagController : ProductsController, IProductTagAppService
    {
        private readonly IProductTagAppService _service;

        public ProductTagController(IProductTagAppService service)
        {
            _service = service;
        }
        
        [HttpGet]
        [Route("{id}")]
        [RemoteService(false)]
        public Task<ProductTagDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductTagDto>> GetListAsync(GetProductTagListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}