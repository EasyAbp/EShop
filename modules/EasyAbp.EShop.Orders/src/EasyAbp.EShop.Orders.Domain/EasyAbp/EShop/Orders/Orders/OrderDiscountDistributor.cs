using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaMoney;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountDistributor : IOrderDiscountDistributor, ITransientDependency
{
    protected IMoneyDistributor MoneyDistributor { get; }

    public OrderDiscountDistributor(IMoneyDistributor moneyDistributor)
    {
        MoneyDistributor = moneyDistributor;
    }

    public virtual async Task<OrderDiscountDistributionModel> DistributeAsync(IOrder order,
        Dictionary<IOrderLine, decimal> currentTotalPrices, OrderDiscountInfoModel discount)
    {
        var affectedCurrentTotalPrices = discount.AffectedOrderLineIds
            .Select(orderLineId => order.OrderLines.Single(x => x.Id == orderLineId))
            .ToDictionary(x => x, x => currentTotalPrices[x]);

        var affectedPriceSum = new Money(affectedCurrentTotalPrices.Sum(x => x.Value), order.Currency);

        var totalDiscountAmount = discount.CalculateDiscountAmount(affectedPriceSum.Amount, order.Currency);

        var result = await MoneyDistributor.DistributeAsync(
            order.Currency, affectedCurrentTotalPrices, -totalDiscountAmount);

        foreach (var affectedCurrentTotalPrice in affectedCurrentTotalPrices)
        {
            currentTotalPrices[affectedCurrentTotalPrice.Key] = affectedCurrentTotalPrice.Value;
        }

        // revert to positive amount
        return new OrderDiscountDistributionModel(
            discount,
            currentTotalPrices,
            result.Distributions.ToDictionary(x => x.Key, x => -x.Value)
        );
    }
}