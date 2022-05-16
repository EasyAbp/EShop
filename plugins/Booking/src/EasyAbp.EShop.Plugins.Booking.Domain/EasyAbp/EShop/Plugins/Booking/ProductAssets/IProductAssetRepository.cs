using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public interface IProductAssetRepository : IRepository<ProductAsset, Guid>
    {
    }
}