using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetails.Dtos
{
    [Serializable]
    public class ProductDetailDto : ExtensibleEntityDto<Guid>
    {
        public string Description { get; set; }
    }
}