using System;
using System.ComponentModel;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.ProductDetails.Dtos
{
    [Serializable]
    public class CreateUpdateProductDetailDto : ExtensibleObject
    {
        [DisplayName("ProductDetailStoreId")]
        public Guid? StoreId { get; set; }

        [DisplayName("ProductDetailDescription")]
        public string Description { get; set; }
    }
}