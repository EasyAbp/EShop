using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using NodaMoney;

namespace EasyAbp.EShop.Orders.Orders
{
    public interface IOrderExtraFeeProvider
    {
        Task<List<OrderExtraFeeInfoModel>> GetListAsync(Guid customerUserId, ICreateOrderInfo input,
            Dictionary<Guid, IProduct> productDict, Currency effectiveCurrency);
    }
}