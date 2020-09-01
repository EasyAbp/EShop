using System;

namespace EasyAbp.EShop.Orders.Orders
{
    [Serializable]
    public class OrderLineEto : IOrderLine
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public DateTime ProductModificationTime { get; set; }
        
        public DateTime ProductDetailModificationTime { get; set; }
        
        public string ProductGroupName { get; set; }
        
        public string ProductUniqueName { get; set; }

        public string ProductDisplayName { get; set; }
        
        public string SkuName { get; set; }

        public string SkuDescription { get; set; }
        
        public string MediaResources { get; set; }
        
        public string Currency { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public decimal TotalDiscount { get; set; }
        
        public decimal ActualTotalPrice { get; set; }

        public int Quantity { get; set; }
        
        public int RefundedQuantity { get; set; }
        
        public decimal RefundAmount { get; set; }
    }
}