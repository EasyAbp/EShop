using System;
using System.ComponentModel;

namespace EasyAbp.EShop.Products.ProductDetails.Dtos
{
    public class CreateUpdateProductDetailDto
    {
        /// <summary>
        /// This property is for product management permission checking
        /// </summary>
        [DisplayName("ProductDetailStoreId")]
        public Guid StoreId { get; set; }

        [DisplayName("ProductDetailDescription")]
        public string Description { get; set; }
    }
}