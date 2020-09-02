using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class ProductAttributeOptionDto : ExtensibleFullAuditedEntityDto<Guid>
    {
        [Required]
        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }
    }
}