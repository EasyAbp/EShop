using System;
using System.ComponentModel;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class CreateOrderLineDto : ExtensibleObject
    {
        [DisplayName("OrderLineProductId")]
        public Guid ProductId { get; set; }
        
        [DisplayName("OrderLineProductSkuId")]
        public Guid ProductSkuId { get; set; }
        
        [DisplayName("OrderLineQuantity")]
        public int Quantity { get; set; }
    }
}