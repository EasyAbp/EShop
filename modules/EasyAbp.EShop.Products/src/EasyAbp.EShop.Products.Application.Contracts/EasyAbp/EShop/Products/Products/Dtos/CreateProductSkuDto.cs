using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class CreateProductSkuDto : UpdateProductSkuDto
    {
        [Required]
        [DisplayName("ProductSkuAttributeOptionIds")]
        public List<Guid> AttributeOptionIds { get; set; }
    }
}