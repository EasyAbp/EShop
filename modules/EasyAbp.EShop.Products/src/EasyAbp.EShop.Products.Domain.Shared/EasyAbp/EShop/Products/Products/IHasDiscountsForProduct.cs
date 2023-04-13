using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public interface IHasDiscountsForProduct
{
    /// <summary>
    /// The Price of the ProductSku has been subtracted from these product discounts.
    /// </summary>
    List<ProductDiscountInfoModel> ProductDiscounts { get; }

    /// <summary>
    /// These order discount previews do not change the Price. They will be effective after placing an order.
    /// </summary>
    List<OrderDiscountPreviewInfoModel> OrderDiscountPreviews { get; }
}