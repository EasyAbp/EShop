using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class CreateUpdateProductAttributeOptionDto : ExtensibleObject
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