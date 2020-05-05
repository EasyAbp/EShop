using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    public class OrderLineDto : FullAuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }
        
        public Guid ProductSkuId { get; set; }
        
        public DateTime ProductModificationTime { get; set; }
        
        public DateTime ProductDetailModificationTime { get; set; }
        
        public string ProductName { get; set; }
        
        public string SkuDescription { get; set; }
        
        public string MediaResources { get; set; }
        
        public string Currency { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public decimal TotalPrice { get; set; }
        
        public decimal TotalDiscount { get; set; }

        public int Quantity { get; set; }
    }
}