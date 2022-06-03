using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public interface IStoreAssetCategoryRepository : IRepository<StoreAssetCategory, Guid>
    {
    }
}