using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    public class CreateUpdateCategoryDto : ExtensibleObject
    {
        [DisplayName("CategoryUniqueName")]
        public string UniqueName { get; set; }
        
        [Required]
        [DisplayName("CategoryDisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("CategoryDescription")]
        public string Description { get; set; }

        [DisplayName("CategoryMediaResources")]
        public string MediaResources { get; set; }
    }
}