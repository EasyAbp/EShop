using System;
using System.ComponentModel;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.ProductDetails.Dtos
{
    public class CreateUpdateProductDetailDto : ExtensibleObject
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