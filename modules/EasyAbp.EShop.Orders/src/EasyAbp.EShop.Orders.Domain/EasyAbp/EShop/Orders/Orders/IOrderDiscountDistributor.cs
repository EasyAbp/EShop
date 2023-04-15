using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders;

public interface IOrderDiscountDistributor
{
    Task<OrderDiscountDistributionModel> DistributeAsync(IOrder order, Dictionary<IOrderLine, decimal> currentPrices,
        OrderDiscountInfoModel discount);
}