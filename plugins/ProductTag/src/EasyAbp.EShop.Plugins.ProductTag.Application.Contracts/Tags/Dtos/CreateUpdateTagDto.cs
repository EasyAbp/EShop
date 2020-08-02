using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.ProductTag.Tags.Dtos
{
    public class UpdateTagDto : ExtensibleEntityDto
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