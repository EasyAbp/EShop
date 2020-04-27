using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductCategories.Dtos
{
    public class ProductCategoryDto : AuditedEntityDto<Guid>
    {
        public Guid StoreId { get; set; }

        public Guid CategoryId { get; set; }

        public Guid ProductId { get; set; }

        public int DisplayOrder { get; set; }
    }
}