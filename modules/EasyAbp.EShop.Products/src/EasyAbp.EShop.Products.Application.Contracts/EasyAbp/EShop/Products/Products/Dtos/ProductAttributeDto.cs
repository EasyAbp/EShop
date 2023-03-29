using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class ProductAttributeDto : ExtensibleFullAuditedEntityDto<Guid>, IProductAttribute
    {
        [Required]
        public string DisplayName { get; set; }
        
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }

        IEnumerable<IProductAttributeOption> IProductAttribute.ProductAttributeOptions => ProductAttributeOptions;
        public List<ProductAttributeOptionDto> ProductAttributeOptions { get; set; }
    }
}