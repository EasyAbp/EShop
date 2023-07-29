using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NodaMoney;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class MoneyDistributor : IMoneyDistributor, ITransientDependency
{
    public virtual Task<MoneyDistributionResult<TKey>> DistributeAsync<TKey>(string currency,
        Dictionary<TKey, decimal> currentAmounts, decimal totalDistributionAmount)
    {
        var distributions = new Dictionary<TKey, decimal>();
        var originalAmountSum = currentAmounts.Sum(x => x.Value);
        var remainingDistributionAmount = totalDistributionAmount;

        foreach (var key in currentAmounts.Keys)
        {
            var calculatedDistributionAmount = new Money(
                currentAmounts[key] / originalAmountSum *
                totalDistributionAmount, currency, MidpointRounding.ToZero);

            var distributionAmount = currentAmounts[key] + calculatedDistributionAmount.Amount < 0
                ? currentAmounts[key]
                : calculatedDistributionAmount.Amount;

            distributions[key] = distributionAmount;
            currentAmounts[key] += distributionAmount;
            remainingDistributionAmount -= distributionAmount;
        }

        foreach (var key in currentAmounts.OrderByDescending(x => x.Value).Select(x => x.Key))
        {
            if (remainingDistributionAmount == decimal.Zero)
            {
                break;
            }

            var distributionAmount = currentAmounts[key] + remainingDistributionAmount < 0
                ? currentAmounts[key]
                : remainingDistributionAmount;

            distributions[key] += distributionAmount;
            currentAmounts[key] += distributionAmount;
            remainingDistributionAmount -= distributionAmount;
        }

        if (remainingDistributionAmount != decimal.Zero)
        {
            throw new ApplicationException("The MoneyDistributor failed to distribute the remaining");
        }

        return Task.FromResult(new MoneyDistributionResult<TKey>(currency, currentAmounts, distributions));
    }
}