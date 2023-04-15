using System;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class UpdatePromotionDto
{
    public string DisplayName { get; set; }

    public string Configurations { get; set; }

    public DateTime? FromTime { get; set; }

    public DateTime? ToTime { get; set; }

    public bool Disabled { get; set; }

    public int Priority { get; set; }

    public UpdatePromotionDto()
    {
    }

    public UpdatePromotionDto(string displayName, string configurations, DateTime? fromTime, DateTime? toTime,
        bool disabled, int priority)
    {
        DisplayName = displayName;
        Configurations = configurations;
        FromTime = fromTime;
        ToTime = toTime;
        Disabled = disabled;
        Priority = priority;
    }
}