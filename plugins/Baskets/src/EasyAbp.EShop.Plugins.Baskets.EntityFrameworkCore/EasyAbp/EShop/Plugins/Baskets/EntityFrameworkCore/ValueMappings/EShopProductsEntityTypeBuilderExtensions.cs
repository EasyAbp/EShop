using System;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.ValueMappings;

public static class EShopProductsEntityTypeBuilderExtensions
{
    public static void TryConfigureDiscountsInfo(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<IHasDiscountsInfo>())
        {
            b.Property(nameof(IHasDiscountsInfo.ProductDiscounts))
                .HasConversion<ProductDiscountsInfoValueConverter>()
                .Metadata.SetValueComparer(new ProductDiscountsInfoValueComparer());
            b.Property(nameof(IHasDiscountsInfo.OrderDiscountPreviews))
                .HasConversion<OrderDiscountPreviewsInfoValueConverter>()
                .Metadata.SetValueComparer(new OrderDiscountPreviewsInfoValueComparer());
        }
    }
}