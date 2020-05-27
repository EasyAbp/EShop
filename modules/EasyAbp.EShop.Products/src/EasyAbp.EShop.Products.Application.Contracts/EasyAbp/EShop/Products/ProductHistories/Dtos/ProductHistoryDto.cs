using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.ProductHistories.Dtos
{
    public class ProductHistoryDto : EntityDto<Guid>
    {
        public Guid ProductId { get; set; }

        public DateTime ModificationTime { get; set; }

        public string SerializedEntityData { get; set; }
    }
}