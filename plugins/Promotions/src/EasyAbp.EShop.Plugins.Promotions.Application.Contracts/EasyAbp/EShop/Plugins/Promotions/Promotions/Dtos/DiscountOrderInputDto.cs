using System;
using EasyAbp.EShop.Orders.Orders;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class DiscountOrderInputDto
{
    public OrderDiscountContext Context { get; set; }

    public DiscountOrderInputDto()
    {
    }

    public DiscountOrderInputDto(OrderDiscountContext context)
    {
        Context = context;
    }
}