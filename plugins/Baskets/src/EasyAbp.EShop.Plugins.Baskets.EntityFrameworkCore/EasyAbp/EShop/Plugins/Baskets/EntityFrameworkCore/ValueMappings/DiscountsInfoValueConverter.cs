using System.Collections.Generic;
using System.Text.Json;
using EasyAbp.EShop.Products.Products;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.ValueMappings;

public class ProductDiscountsInfoValueConverter : ValueConverter<List<ProductDiscountInfoModel>, string>
{
    public ProductDiscountsInfoValueConverter() : base(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => JsonSerializer.Deserialize<List<ProductDiscountInfoModel>>(v, (JsonSerializerOptions)null))
    {
    }
}

public class OrderDiscountPreviewsInfoValueConverter : ValueConverter<List<OrderDiscountPreviewInfoModel>, string>
{
    public OrderDiscountPreviewsInfoValueConverter() : base(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => JsonSerializer.Deserialize<List<OrderDiscountPreviewInfoModel>>(v, (JsonSerializerOptions)null))
    {
    }
}