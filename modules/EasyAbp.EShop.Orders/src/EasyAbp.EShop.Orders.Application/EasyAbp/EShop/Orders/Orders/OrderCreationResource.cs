using System;
using System.Collections.Generic;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderCreationResource
    {
        public ICreateOrderInfo Input { get; set; }
        
        public Dictionary<Guid, ProductDto> ProductDictionary { get; set; }
    }
}