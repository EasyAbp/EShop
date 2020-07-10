using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class BasketItemDto : AuditedEntityDto<Guid>
    {
        public string BasketName { get; set; }

        public Guid UserId { get; set; }
        
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public int Quantity { get; set; }

        public string MediaResources { get; set; }

        public string ProductUniqueName { get; set; }
        
        public string ProductDisplayName { get; set; }

        public string SkuName { get; set; }
        
        public string SkuDescription { get; set; }

        public string Currency { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal TotalDiscount { get; set; }
        
        public int Inventory { get; set; }
        
        public bool IsInvalid { get; set; }
    }
}