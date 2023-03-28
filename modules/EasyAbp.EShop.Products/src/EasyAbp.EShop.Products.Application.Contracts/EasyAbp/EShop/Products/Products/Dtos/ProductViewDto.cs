using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductViewDto : ExtensibleCreationAuditedEntityDto<Guid>, IProductBase
    {
        public Guid StoreId { get; set; }

        public string ProductGroupName { get; set; }

        public Guid? ProductDetailId { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public string Overview { get; set; }

        public InventoryStrategy InventoryStrategy { get; set; }

        public string InventoryProviderName { get; set; }

        public string MediaResources { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsPublished { get; set; }

        public bool IsStatic { get; set; }

        public bool IsHidden { get; set; }

        public decimal? MinimumPrice { get; set; }

        public decimal? MaximumPrice { get; set; }

        public long Sold { get; set; }

        public string ProductGroupDisplayName { get; set; }
    }
}