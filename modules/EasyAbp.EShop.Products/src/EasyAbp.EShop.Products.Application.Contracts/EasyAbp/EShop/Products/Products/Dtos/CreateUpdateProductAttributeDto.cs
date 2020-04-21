using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductAttributeDto
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