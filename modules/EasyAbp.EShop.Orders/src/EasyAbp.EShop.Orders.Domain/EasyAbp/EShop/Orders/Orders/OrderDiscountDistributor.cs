using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using NodaMoney;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountDistributor : IOrderDiscountDistributor, ITransientDependency
{
    public virtual Task<OrderDiscountDistributionModel> DistributeAsync(IOrder order,
        Dictionary<IOrderLine, decimal> currentPrices, OrderDiscountInfoModel discount)
    {
        var affectedOrderLines = discount.AffectedOrderLineIds
            .Select(orderLineId => order.OrderLines.Single(x => x.Id == orderLineId))
            .ToList();

        var affectedOrderLinesCurrentPrice =
            new Money(affectedOrderLines.Sum(x => currentPrices[x]), order.Currency);

        var totalDiscountAmount = discount.CalculateDiscountAmount(affectedOrderLinesCurrentPrice.Amount, order.Currency);

        var distributions = new Dictionary<Guid, decimal>();
        var remainingDiscountAmount = totalDiscountAmount;

        foreach (var orderLine in affectedOrderLines)
        {
            var calculatedDiscountAmount = new Money(
                currentPrices[orderLine] / affectedOrderLinesCurrentPrice.Amount *
                totalDiscountAmount, order.Currency, MidpointRounding.ToZero);

            var discountAmount = calculatedDiscountAmount.Amount > currentPrices[orderLine]
                ? currentPrices[orderLine]
                : calculatedDiscountAmount.Amount;

            distributions[orderLine.Id] = discountAmount;
            currentPrices[orderLine] -= discountAmount;
            remainingDiscountAmount -= discountAmount;
        }

        foreach (var orderLine in affectedOrderLines.OrderByDescending(x => currentPrices[x]))
        {
            if (remainingDiscountAmount == decimal.Zero)
            {
                break;
            }

            var discountAmount = remainingDiscountAmount > currentPrices[orderLine]
                ? currentPrices[orderLine]
                : remainingDiscountAmount;

            distributions[orderLine.Id] += discountAmount;
            currentPrices[orderLine] -= discountAmount;
            remainingDiscountAmount -= discountAmount;
        }

        if (remainingDiscountAmount != decimal.Zero)
        {
            throw new ApplicationException("The OrderDiscountDistributor failed to distribute the remaining");
        }

        return Task.FromResult(new OrderDiscountDistributionModel(discount, distributions));
    }
}