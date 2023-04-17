using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountProductOutputDto
{
    public GetProductsRealTimePriceContext Context { get; set; }

    public DiscountProductOutputDto()
    {
    }

    public DiscountProductOutputDto(GetProductsRealTimePriceContext context)
    {
        Context = context;
    }
}