using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface INewOrderGenerator
    {
        Task<Order> GenerateAsync(Guid customerUserId, CreateOrderDto input, Dictionary<Guid, ProductDto> productDict,
            Dictionary<Guid, ProductDetailDto> productDetailDict);
    }
}