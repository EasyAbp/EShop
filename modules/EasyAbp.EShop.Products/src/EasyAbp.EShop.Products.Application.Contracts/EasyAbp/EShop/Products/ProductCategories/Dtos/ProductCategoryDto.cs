using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductCategories.Dtos
{
    [Serializable]
    public class ProductCategoryDto : ExtensibleAuditedEntityDto<Guid>
    {
        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; }

        public int DisplayOrder { get; set; }
    }
}