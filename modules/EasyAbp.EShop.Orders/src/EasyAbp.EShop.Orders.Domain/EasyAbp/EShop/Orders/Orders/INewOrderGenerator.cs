using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface INewOrderGenerator
    {
        Task<Order> GenerateAsync(Guid customerUserId, ICreateOrderInfo input, Dictionary<Guid, IProduct> productDict,
            Dictionary<Guid, DateTime> productDetailModificationTimeDict);
    }
}