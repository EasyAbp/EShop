using System;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.ValueMappings;

public static class EShopProductsEntityTypeBuilderExtensions
{
    public static void TryConfigureAttributeOptionIds(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<IHasAttributeOptionIds>())
        {
            b.Property(nameof(IHasAttributeOptionIds.AttributeOptionIds))
                .HasConversion<AttributeOptionIdsValueConverter>()
                .Metadata.SetValueComparer(new AttributeOptionIdsValueComparer());
        }
    }

    public static void TryConfigureDiscountsInfo(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<IHasDiscountsForSku>())
        {
            b.Property(nameof(IHasDiscountsForSku.ProductDiscounts))
                .HasConversion<ProductDiscountsInfoValueConverter>()
                .Metadata.SetValueComparer(new ProductDiscountsInfoValueComparer());
            b.Property(nameof(IHasDiscountsForSku.OrderDiscountPreviews))
                .HasConversion<OrderDiscountPreviewsInfoValueConverter>()
                .Metadata.SetValueComparer(new OrderDiscountPreviewsInfoValueComparer());
        }
    }
}