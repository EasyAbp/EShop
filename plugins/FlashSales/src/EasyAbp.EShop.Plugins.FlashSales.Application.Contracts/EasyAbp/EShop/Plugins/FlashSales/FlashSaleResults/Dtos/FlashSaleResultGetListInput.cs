using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults.Dtos;

public class FlashSaleResultGetListInput : ExtensiblePagedAndSortedResultRequestDto
{
    public virtual Guid? StoreId { get; set; }

    public virtual Guid? PlanId { get; set; }

    public virtual FlashSaleResultStatus? Status { get; set; }

    public virtual Guid? UserId { get; set; }

    public virtual Guid? OrderId { get; set; }
}
