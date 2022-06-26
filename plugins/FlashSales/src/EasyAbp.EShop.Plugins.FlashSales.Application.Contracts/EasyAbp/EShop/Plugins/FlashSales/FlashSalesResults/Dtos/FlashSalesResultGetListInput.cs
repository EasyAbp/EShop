using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults.Dtos;

public class FlashSalesResultGetListInput : ExtensiblePagedAndSortedResultRequestDto
{
    public virtual Guid? StoreId { get; set; }

    public virtual Guid? PlanId { get; set; }

    public virtual FlashSalesResultStatus? Status { get; set; }

    public virtual Guid? UserId { get; set; }

    public virtual Guid? OrderId { get; set; }
}
