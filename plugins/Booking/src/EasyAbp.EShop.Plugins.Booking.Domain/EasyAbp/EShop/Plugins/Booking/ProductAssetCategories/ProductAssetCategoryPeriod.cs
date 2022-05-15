using System;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;

public class ProductAssetCategoryPeriod : Entity<Guid>
{
    public virtual Guid PeriodId { get; protected set; }
    
    public virtual decimal Price { get; protected set; }
}