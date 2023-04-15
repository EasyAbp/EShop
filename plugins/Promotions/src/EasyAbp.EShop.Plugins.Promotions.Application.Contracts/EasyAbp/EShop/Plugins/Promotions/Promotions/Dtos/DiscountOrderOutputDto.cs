using System;
using EasyAbp.EShop.Orders.Orders;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountOrderOutputDto
{
    public OrderDiscountContext Context { get; set; }

    public DiscountOrderOutputDto()
    {
    }

    public DiscountOrderOutputDto(OrderDiscountContext context)
    {
        Context = context;
    }
}