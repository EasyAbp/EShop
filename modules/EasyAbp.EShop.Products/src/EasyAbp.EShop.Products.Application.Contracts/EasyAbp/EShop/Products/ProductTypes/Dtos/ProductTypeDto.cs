using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductTypes.Dtos
{
    public class ProductTypeDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public MultiTenancySides MultiTenancySide { get; set; }
    }
}