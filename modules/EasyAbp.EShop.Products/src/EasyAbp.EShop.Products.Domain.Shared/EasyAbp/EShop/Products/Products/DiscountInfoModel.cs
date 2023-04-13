using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public abstract class DiscountInfoModel : IDiscountInfo
{
    public string EffectGroup { get; set; }

    public string Name { get; set; }

    public string Key { get; set; }

    public string DisplayName { get; set; }

    /// <summary>
    /// When the discount begins to take effect.
    /// </summary>
    public DateTime? FromTime { get; set; }

    /// <summary>
    /// When the discount ends.
    /// </summary>
    public DateTime? ToTime { get; set; }

    public DiscountInfoModel()
    {
    }

    public DiscountInfoModel([CanBeNull] string effectGroup, [NotNull] string name, [CanBeNull] string key,
        [CanBeNull] string displayName, DateTime? fromTime, DateTime? toTime)
    {
        if (fromTime > toTime)
        {
            throw new InvalidTimePeriodException();
        }

        EffectGroup = effectGroup;
        Name = name;
        Key = key;
        DisplayName = displayName;
        FromTime = fromTime;
        ToTime = toTime;
    }
}