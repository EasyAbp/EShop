using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderDiscountProvider
    {
        Task<List<OrderDiscountInfoModel>> GetAllAsync(Order order, Dictionary<Guid, IProduct> productDict);
    }
}