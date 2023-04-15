using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class PromotionGetListInput : PagedAndSortedResultRequestDto
{
    public Guid? StoreId { get; set; }

    public string? PromotionType { get; set; }

    public string? UniqueName { get; set; }

    public string? DisplayName { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public bool? Disabled { get; set; }
}