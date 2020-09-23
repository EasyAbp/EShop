using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductInventories
{
    [RemoteService(Name = "EasyAbpEShopProducts")]
    [Route("/api/eShop/products/productInventory")]
    public class ProductInventoryController : ProductsController, IProductInventoryAppService
    {
        private readonly IProductInventoryAppService _service;

        public ProductInventoryController(IProductInventoryAppService service)
        {
            _service = service;
        }

        [HttpGet]
        public Task<ProductInventoryDto> GetAsync(Guid productId, Guid productSkuId)
        {
            return _service.GetAsync(productId, productSkuId);
        }

        [HttpPut]
        public Task<ProductInventoryDto> UpdateAsync(UpdateProductInventoryDto input)
        {
            return _service.UpdateAsync(input);
        }
    }
}