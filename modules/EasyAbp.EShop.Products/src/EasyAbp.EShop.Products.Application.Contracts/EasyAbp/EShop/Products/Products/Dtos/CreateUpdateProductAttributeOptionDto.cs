using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductAttributeOptionDto
    {
        [Required]
        [DisplayName("ProductAttributeOptionDisplayName")]
        public string DisplayName { get; set; }
        
        [DisplayName("ProductAttributeOptionDescription")]
        public string Description { get; set; }
        
        [DisplayName("ProductAttributeOptionDisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}