using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Tags.Dtos
{
    public class UpdateTagDto : ExtensibleObject
    {
        [DisplayName("TagParentId")]
        public Guid? ParentId { get; set; }

        [Required]
        [DisplayName("TagDisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("TagDescription")]
        public string Description { get; set; }

        [DisplayName("TagMediaResources")]
        public string MediaResources { get; set; }
    }

    public class CreateTagDto : UpdateTagDto
    {
        [Required]
        [DisplayName("TagStoreId")]
        public Guid StoreId { get; set; }
    }
}