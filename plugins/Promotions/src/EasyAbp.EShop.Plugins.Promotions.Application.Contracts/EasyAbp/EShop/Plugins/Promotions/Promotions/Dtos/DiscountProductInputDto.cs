using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountProductInputDto
{
    public GetProductsRealTimePriceContext Context { get; set; }

    public DiscountProductInputDto()
    {
    }

    public DiscountProductInputDto(GetProductsRealTimePriceContext context)
    {
        Context = context;
    }
}