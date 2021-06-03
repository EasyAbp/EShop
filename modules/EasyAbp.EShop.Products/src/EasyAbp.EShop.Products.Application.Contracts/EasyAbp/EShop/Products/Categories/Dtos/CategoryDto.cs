using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    [Serializable]
    public class CategoryDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        public string UniqueName { get; set; }
        
        public string DisplayName { get; set; }
        
        [NotNull]
        public string Code { get; set; }
        
        public int Level { get; set; }
        
        public Guid? ParentId { get; set; }
        
        public ICollection<CategoryDto> Children { get; set; }

        public string Description { get; set; }

        public string MediaResources { get; set; }
        
        public bool IsHidden { get; set; }
    }
}