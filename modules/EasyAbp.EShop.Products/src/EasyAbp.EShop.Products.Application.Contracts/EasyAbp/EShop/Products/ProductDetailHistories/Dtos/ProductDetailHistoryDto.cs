using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetailHistories.Dtos
{
    public class ProductDetailHistoryDto : ExtensibleEntityDto<Guid>
    {
        public Guid ProductDetailId { get; set; }

        public DateTime ModificationTime { get; set; }

        public string SerializedEntityData { get; set; }
    }
}