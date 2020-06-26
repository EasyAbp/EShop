using System;
using EasyAbp.Abp.Trees.Dtos;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Tags.Dtos
{
    public class TagDto : ExtensibleFullAuditedEntityDto<Guid>, ITreeDto
    {
        public Guid StoreId { get; set; }
        
        public Guid? ParentId { get; set; }
        
        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public string MediaResources { get; set; }
    }
}