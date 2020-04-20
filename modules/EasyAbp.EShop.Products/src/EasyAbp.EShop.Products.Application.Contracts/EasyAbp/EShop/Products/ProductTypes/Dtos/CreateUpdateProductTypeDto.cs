using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductTypes.Dtos
{
    public class CreateUpdateProductTypeDto
    {
        [Required]
        [DisplayName("ProductTypeName")]
        public string Name { get; set; }

        [Required]
        [DisplayName("ProductTypeDisplayName")]
        public string DisplayName { get; set; }

        [DisplayName("ProductTypeDescription")]
        public string Description { get; set; }

        [DisplayName("ProductTypeMultiTenancySide")]
        public MultiTenancySides MultiTenancySide { get; set; }
    }
}