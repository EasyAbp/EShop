using System;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class CreatePromotionDto : IMultiStore
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

    public CreatePromotionDto()
    {
    }

    public CreatePromotionDto(Guid storeId, string promotionType, string uniqueName, string displayName,
        string configurations, DateTime? fromTime, DateTime? toTime, bool disabled, int priority)
    {
        StoreId = storeId;
        PromotionType = promotionType;
        UniqueName = uniqueName;
        DisplayName = displayName;
        Configurations = configurations;
        FromTime = fromTime;
        ToTime = toTime;
        Disabled = disabled;
        Priority = priority;
    }
}