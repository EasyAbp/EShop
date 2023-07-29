using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Orders.Orders;

public interface IMoneyDistributor
{
    Task<MoneyDistributionResult<TKey>> DistributeAsync<TKey>(string currency, Dictionary<TKey, decimal> currentAmounts,
        decimal totalDistributionAmount);
}