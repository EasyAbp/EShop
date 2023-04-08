using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public abstract class DiscountInfoModel
{
    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public string Key { get; set; }

    [CanBeNull]
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

    public DiscountInfoModel([NotNull] string name, [CanBeNull] string key, [CanBeNull] string displayName,
        DateTime? fromTime, DateTime? toTime)
    {
        if (fromTime > toTime)
        {
            throw new InvalidTimePeriodException();
        }

        Name = name;
        Key = key;
        DisplayName = displayName;
        FromTime = fromTime;
        ToTime = toTime;
    }
}