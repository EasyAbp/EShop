using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.ProductCategories.Dtos
{
    public class CreateUpdateProductCategoryDto
    {
        [Required]
        [DisplayName("ProductCategoryCategoryId")]
        public Guid CategoryId { get; set; }

        [Required]
        [DisplayName("ProductCategoryProductId")]
        public Guid ProductId { get; set; }

        [DisplayName("ProductCategoryDisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}