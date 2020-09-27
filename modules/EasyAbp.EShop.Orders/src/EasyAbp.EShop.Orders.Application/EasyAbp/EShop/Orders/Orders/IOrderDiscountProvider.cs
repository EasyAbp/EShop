using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderDiscountProvider
    {
        Task<Order> DiscountAsync(Order order, Dictionary<Guid, ProductDto> productDict);
    }
}