using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountsSchemeModel
{
    public List<ProductDiscountInfoModel> Discounts { get; }

    public decimal TotalDiscountAmount => Discounts.Where(x => x.InEffect).Sum(x => x.DiscountedAmount);

    public ProductDiscountsSchemeModel(List<ProductDiscountInfoModel> discounts)
    {
        Discounts = discounts;
    }
}