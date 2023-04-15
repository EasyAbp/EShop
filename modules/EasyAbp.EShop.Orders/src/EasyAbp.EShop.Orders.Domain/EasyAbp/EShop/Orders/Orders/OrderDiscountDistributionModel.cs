using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountDistributionModel
{
    public OrderDiscountInfoModel DiscountInfoModel { get; set; }

    /// <summary>
    /// OrderLine to discount amount mapping.
    /// </summary>
    public Dictionary<Guid, decimal> Distributions { get; set; }

    public OrderDiscountDistributionModel(OrderDiscountInfoModel discountInfoModel,
        Dictionary<Guid, decimal> distributions)
    {
        DiscountInfoModel = Check.NotNull(discountInfoModel, nameof(discountInfoModel));
        Distributions = Check.NotNull(distributions, nameof(distributions));

        if (DiscountInfoModel.AffectedOrderLineIds.Any(x => !Distributions.ContainsKey(x)))
        {
            throw new AbpException("The OrderDiscountDistributionModel got incorrect distributions.");
        }
    }
}