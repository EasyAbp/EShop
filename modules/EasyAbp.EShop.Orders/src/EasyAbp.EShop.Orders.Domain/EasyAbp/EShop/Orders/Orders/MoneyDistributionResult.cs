using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders;

public class MoneyDistributionResult<TKey>
{
    [NotNull]
    public string Currency { get; }

    public Dictionary<TKey, decimal> AmountsAfterDistribution { get; }

    public Dictionary<TKey, decimal> Distributions { get; }

    public MoneyDistributionResult(
        [NotNull] string currency,
        Dictionary<TKey, decimal> amountsAfterDistribution,
        Dictionary<TKey, decimal> distributions)
    {
        Currency = Check.NotNullOrWhiteSpace(currency, nameof(currency));
        AmountsAfterDistribution = Check.NotNull(amountsAfterDistribution, nameof(amountsAfterDistribution));
        Distributions = Check.NotNull(distributions, nameof(distributions));
    }
}