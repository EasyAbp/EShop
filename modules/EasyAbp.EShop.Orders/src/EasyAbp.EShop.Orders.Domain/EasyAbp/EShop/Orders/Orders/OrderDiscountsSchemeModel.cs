using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountsSchemeModel
{
    public List<OrderDiscountDistributionModel> Distributions { get; }

    public decimal TotalDiscountAmount => Distributions.SelectMany(x => x.Distributions).Sum(x => x.Value);

    public OrderDiscountsSchemeModel(List<OrderDiscountDistributionModel> distributions)
    {
        Distributions = distributions;
    }
}