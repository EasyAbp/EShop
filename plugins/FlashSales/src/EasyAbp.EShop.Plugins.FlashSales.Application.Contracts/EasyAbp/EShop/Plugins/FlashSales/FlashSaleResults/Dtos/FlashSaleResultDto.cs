using System;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;

public class FlashSaleResultDto : ExtensibleFullAuditedEntityDto<Guid>, IMultiStore
{
    public Guid StoreId { get; set; }

    public Guid PlanId { get; set; }

    public FlashSaleResultStatus Status { get; set; }

    public string Reason { get; set; }

    public Guid UserId { get; set; }

    public Guid? OrderId { get; set; }

    public DateTime ReducedInventoryTime { get; set; }
}
