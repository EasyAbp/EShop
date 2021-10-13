using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class CreateOrderLineDto : ExtensibleObject
    {
        public const int MinimumQuantity = 1;
        public const int MaximumQuantity = int.MaxValue;
        
        [DisplayName("OrderLineProductId")]
        public Guid ProductId { get; set; }
        
        [DisplayName("OrderLineProductSkuId")]
        public Guid ProductSkuId { get; set; }
        
        [DisplayName("OrderLineQuantity")]
        [Range(MinimumQuantity, MaximumQuantity)]
        public int Quantity { get; set; }
    }
}