using System;
using System.Collections.Generic;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderCreationResource
    {
        public CreateOrderDto Input { get; set; }
        
        public Dictionary<Guid, ProductDto> ProductDictionary { get; set; }
    }
}