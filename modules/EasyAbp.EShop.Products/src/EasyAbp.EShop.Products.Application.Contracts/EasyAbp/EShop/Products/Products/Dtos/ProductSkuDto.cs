using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductSkuDto : FullAuditedEntityDto<Guid>
    {
        public string SerializedAttributeOptionIds { get; set; }
        
        public string Code { get; set; }
        
        public string Currency { get; set; }
        
        public decimal? OriginalPrice { get; set; }
        
        /// <summary>
        /// Price property in the ProductSku entity.
        /// </summary>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Price from IProductDiscountManager
        /// </summary>
        public decimal DiscountedPrice { get; set; }
        
        /// <summary>
        /// Inventory property in the ProductSku entity.
        /// </summary>
        public int Inventory { get; set; }
        
        /// <summary>
        /// Inventory provider's inventory quantity (same as Inventory property if there is no provider).
        /// </summary>
        public int RealInventory { get; set; }
        
        public int Sold { get; set; }
        
        public int OrderMinQuantity { get; set; }
        
        public int OrderMaxQuantity { get; set; }
        
        public Guid? ProductDetailId { get; set; }
    }
}