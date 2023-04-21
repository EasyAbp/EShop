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
        Dictionary<IOrderLine, decimal> currentTotalPrices, OrderDiscountInfoModel discount)
    {
        var affectedOrderLines = discount.AffectedOrderLineIds
            .Select(orderLineId => order.OrderLines.Single(x => x.Id == orderLineId))
            .ToList();

        var affectedOrderLinesCurrentTotalPrice =
            new Money(affectedOrderLines.Sum(x => currentTotalPrices[x]), order.Currency);

        var totalDiscountAmount =
            discount.CalculateDiscountAmount(affectedOrderLinesCurrentTotalPrice.Amount, order.Currency);

        var distributions = new Dictionary<Guid, decimal>();
        var remainingDiscountAmount = totalDiscountAmount;

        foreach (var orderLine in affectedOrderLines)
        {
            var calculatedDiscountAmount = new Money(
                currentTotalPrices[orderLine] / affectedOrderLinesCurrentTotalPrice.Amount *
                totalDiscountAmount, order.Currency, MidpointRounding.ToZero);

            var discountAmount = calculatedDiscountAmount.Amount > currentTotalPrices[orderLine]
                ? currentTotalPrices[orderLine]
                : calculatedDiscountAmount.Amount;

            distributions[orderLine.Id] = discountAmount;
            currentTotalPrices[orderLine] -= discountAmount;
            remainingDiscountAmount -= discountAmount;
        }

        foreach (var orderLine in affectedOrderLines.OrderByDescending(x => currentTotalPrices[x]))
        {
            if (remainingDiscountAmount == decimal.Zero)
            {
                break;
            }

            var discountAmount = remainingDiscountAmount > currentTotalPrices[orderLine]
                ? currentTotalPrices[orderLine]
                : remainingDiscountAmount;

            distributions[orderLine.Id] += discountAmount;
            currentTotalPrices[orderLine] -= discountAmount;
            remainingDiscountAmount -= discountAmount;
        }

        if (remainingDiscountAmount != decimal.Zero)
        {
            throw new ApplicationException("The OrderDiscountDistributor failed to distribute the remaining");
        }

        return Task.FromResult(new OrderDiscountDistributionModel(discount, distributions));
    }
}