using System;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;

public class ProductAssetCategoryPeriod : Entity<Guid>
{
    public virtual Guid PeriodId { get; protected set; }
    
    public virtual decimal Price { get; protected set; }

    protected ProductAssetCategoryPeriod()
    {
    }

    public ProductAssetCategoryPeriod(
        Guid id,
        Guid periodId,
        decimal price
    ) : base(id)
    {
        PeriodId = periodId;
        Price = price;
    }
}
