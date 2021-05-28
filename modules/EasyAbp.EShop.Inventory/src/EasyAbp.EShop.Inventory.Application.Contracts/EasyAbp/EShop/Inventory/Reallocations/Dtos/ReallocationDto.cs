using EasyAbp.EShop.Stores.Stores;
using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Inventory.Reallocations.Dtos
{
    [Serializable]
    public class ReallocationDto : FullAuditedEntityDto<Guid>, IMultiStore, IReallocation
    {
        public Guid ProductSkuId { get; set; }

        public Guid SourceWarehouseId { get; set; }

        public Guid DestinationWarehouseId { get; set; }

        public int Units { get; set; }

        public string OperatorName { get; set; }

        public DateTime ReallocationTime { get; set; }

        public string Description { get; set; }

        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }
    }
}