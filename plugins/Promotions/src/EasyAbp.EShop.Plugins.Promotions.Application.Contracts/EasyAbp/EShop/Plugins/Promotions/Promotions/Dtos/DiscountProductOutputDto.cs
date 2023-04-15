using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountProductOutputDto
{
    public ProductDiscountContext Context { get; set; }

    public DiscountProductOutputDto()
    {
    }

    public DiscountProductOutputDto(ProductDiscountContext context)
    {
        Context = context;
    }
}