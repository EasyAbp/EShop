using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    public class CreateOrderLineDto
    {
        [DisplayName("OrderLineProductId")]
        public Guid ProductId { get; set; }
        
        [DisplayName("OrderLineProductSkuId")]
        public Guid ProductSkuId { get; set; }
        
        [DisplayName("OrderLineQuantity")]
        public int Quantity { get; set; }
    }
}