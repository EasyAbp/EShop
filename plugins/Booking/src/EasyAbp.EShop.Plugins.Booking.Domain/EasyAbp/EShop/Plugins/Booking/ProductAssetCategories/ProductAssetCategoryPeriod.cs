using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;

public class ProductAssetCategoryPeriod : Entity<Guid>
{
    public virtual Guid PeriodId { get; protected set; }

    [NotNull]
    public virtual string Currency { get; protected set; }

    public virtual decimal Price { get; protected set; }

    protected ProductAssetCategoryPeriod()
    {
    }

    public ProductAssetCategoryPeriod(
        Guid id,
        Guid periodId,
        [NotNull] string currency,
        decimal price
    ) : base(id)
    {
        PeriodId = periodId;
        Currency = currency;
        Price = price;
    }
}