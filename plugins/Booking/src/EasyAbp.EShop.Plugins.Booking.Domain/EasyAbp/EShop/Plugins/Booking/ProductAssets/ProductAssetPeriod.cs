using System;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets;

public class ProductAssetPeriod : Entity<Guid>
{
    public virtual Guid PeriodId { get; protected set; }
    
    public virtual decimal Price { get; protected set; }
}