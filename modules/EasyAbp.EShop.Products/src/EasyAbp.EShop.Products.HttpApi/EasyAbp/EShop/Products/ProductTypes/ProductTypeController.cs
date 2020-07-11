using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductTypes.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductTypes
{
    [RemoteService(Name = "EShopProducts")]
    [Route("/api/eShop/products/productType")]
    public class ProductTypeController : ProductsController, IProductTypeAppService
    {
        private readonly IProductTypeAppService _service;

        public ProductTypeController(IProductTypeAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}")]
        public Task<ProductTypeDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductTypeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}