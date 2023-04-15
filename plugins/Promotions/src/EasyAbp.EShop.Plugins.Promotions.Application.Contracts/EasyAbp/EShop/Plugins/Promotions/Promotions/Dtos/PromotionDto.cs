using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class PromotionDto : FullAuditedEntityDto<Guid>
{
    public Guid StoreId { get; set; }

    public string PromotionType { get; set; }

    public string UniqueName { get; set; }

    public string DisplayName { get; set; }

    public string Configurations { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public bool Disabled { get; set; }

    public int Priority { get; set; }
}