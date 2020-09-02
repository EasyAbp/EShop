using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductAttributeDto : ExtensibleObject
    {
        [Required]
        [DisplayName("ProductAttributeDisplayName")]
        public string DisplayName { get; set; }
        
        [DisplayName("ProductAttributeDescription")]
        public string Description { get; set; }
        
        [DisplayName("ProductAttributeDisplayOrder")]
        public int DisplayOrder { get; set; }
        
        public ICollection<CreateUpdateProductAttributeOptionDto> ProductAttributeOptions { get; set; }
    }
}