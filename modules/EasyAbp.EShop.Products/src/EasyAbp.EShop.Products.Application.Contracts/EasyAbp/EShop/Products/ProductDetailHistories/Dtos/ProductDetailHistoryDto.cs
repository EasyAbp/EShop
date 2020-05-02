using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductDetailHistories.Dtos
{
    public class ProductDetailHistoryDto : EntityDto<Guid>
    {
        public Guid ProductDetailId { get; set; }

        public DateTime ModificationTime { get; set; }

        public string SerializedDto { get; set; }
    }
}