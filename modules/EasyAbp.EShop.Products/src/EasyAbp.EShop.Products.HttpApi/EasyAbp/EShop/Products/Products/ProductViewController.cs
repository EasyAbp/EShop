using System;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/e-shop/products/product/view")]
    public class ProductViewController : ProductsController, IProductViewAppService
    {
        private readonly IProductViewAppService _service;

        public ProductViewController(IProductViewAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<ProductViewDto>> GetListAsync(GetProductListInput input)
        {
            return _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<ProductViewDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }
    }
}