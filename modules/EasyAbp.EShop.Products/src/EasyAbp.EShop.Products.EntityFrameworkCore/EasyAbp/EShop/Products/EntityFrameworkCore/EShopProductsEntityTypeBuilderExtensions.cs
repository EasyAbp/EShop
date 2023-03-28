using System;
using EasyAbp.EShop.Products.EntityFrameworkCore.AttributeOptionIds;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyAbp.EShop.Products.EntityFrameworkCore;

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
}