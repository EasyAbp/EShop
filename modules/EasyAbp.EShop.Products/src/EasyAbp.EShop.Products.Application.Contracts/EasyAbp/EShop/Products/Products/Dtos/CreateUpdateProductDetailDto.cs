using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    public class CreateUpdateProductDetailDto
    {
        [DisplayName("ProductDetailDescription")]
        public string Description { get; set; }
    }
}