using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;
using NodaMoney;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderExtraFeeProvider
    {
        Task<List<OrderExtraFeeInfoModel>> GetListAsync(Guid customerUserId, CreateOrderDto input,
            Dictionary<Guid, ProductDto> productDict, Currency effectiveCurrency);
    }
}