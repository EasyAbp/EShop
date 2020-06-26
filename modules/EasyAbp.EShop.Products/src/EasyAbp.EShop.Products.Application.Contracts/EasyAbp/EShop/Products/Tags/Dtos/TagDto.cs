using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Tags.Dtos
{
    public class TagDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        public Guid? ParentId { get; set; }

        public Guid StoreId { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MediaResources { get; set; }
    }
}