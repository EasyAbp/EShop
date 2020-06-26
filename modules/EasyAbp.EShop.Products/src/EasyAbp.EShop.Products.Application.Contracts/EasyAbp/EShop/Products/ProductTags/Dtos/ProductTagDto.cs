using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductTags.Dtos
{
    public class ProductTagDto : AuditedEntityDto<Guid>
    {
        public Guid TagId { get; set; }

        public Guid ProductId { get; set; }

        public int DisplayOrder { get; set; }
    }
}