using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;

[Serializable]
public class FlashSalesPlanDto : ExtensibleFullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid StoreId { get; set; }

    public DateTime BeginTime { get; set; }

    public DateTime EndTime { get; set; }

    public Guid ProductId { get; set; }

    public Guid ProductSkuId { get; set; }

    public bool IsPublished { get; set; }
    
    public string ConcurrencyStamp { get; set; }
}
