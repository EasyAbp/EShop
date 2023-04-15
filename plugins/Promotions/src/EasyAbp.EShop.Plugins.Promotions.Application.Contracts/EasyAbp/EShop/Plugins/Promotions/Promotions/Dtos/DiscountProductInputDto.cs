using System;
using EasyAbp.EShop.Products.Products;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountProductInputDto
{
    public ProductDiscountContext Context { get; set; }

    public DiscountProductInputDto()
    {
    }

    public DiscountProductInputDto(ProductDiscountContext context)
    {
        Context = context;
    }
}