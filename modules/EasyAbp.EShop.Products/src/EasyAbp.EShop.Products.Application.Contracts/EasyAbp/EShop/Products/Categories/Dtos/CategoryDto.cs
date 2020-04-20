using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    public class CategoryDto : FullAuditedEntityDto<Guid>
    {
        public Guid? ParentCategoryId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MediaResources { get; set; }
    }
}