using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductCategories
{
    [RemoteService(Name = "EShopProducts")]
    [Route("/api/eShop/products/productCategory")]
    public class ProductCategoryController : ProductsController, IProductCategoryAppService
    {
        private readonly IProductCategoryAppService _service;

        public ProductCategoryController(IProductCategoryAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id}/abandoned")]
        [RemoteService(false)]
        public Task<ProductCategoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<ProductCategoryDto>> GetListAsync(GetProductCategoryListDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}