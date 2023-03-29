using System;

namespace EasyAbp.EShop.Orders.Orders.Dtos;

public class OrderDiscountDto
{
    public Guid OrderLineId { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }

    public string DisplayName { get; set; }

    public decimal DiscountedAmount { get; set; }
}