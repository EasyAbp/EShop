using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountDistributionModel
{
    public OrderDiscountInfoModel DiscountInfoModel { get; set; }

    public Dictionary<IOrderLine, decimal> CurrentTotalPrices { get; set; }

    /// <summary>
    /// OrderLine to discount amount mapping.
    /// </summary>
    public Dictionary<IOrderLine, decimal> Distributions { get; set; }

    public OrderDiscountDistributionModel(
        OrderDiscountInfoModel discountInfoModel,
        Dictionary<IOrderLine, decimal> currentTotalPrices,
        Dictionary<IOrderLine, decimal> distributions)
    {
        DiscountInfoModel = Check.NotNull(discountInfoModel, nameof(discountInfoModel));
        CurrentTotalPrices = Check.NotNull(currentTotalPrices, nameof(currentTotalPrices));
        Distributions = Check.NotNull(distributions, nameof(distributions));

        if (DiscountInfoModel.AffectedOrderLineIds.Any(x => Distributions.Keys.All(y => y.Id != x)))
        {
            throw new AbpException("The OrderDiscountDistributionModel got incorrect distributions.");
        }
    }
}