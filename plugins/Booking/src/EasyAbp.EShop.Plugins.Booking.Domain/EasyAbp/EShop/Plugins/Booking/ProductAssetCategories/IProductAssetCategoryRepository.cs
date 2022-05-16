using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public interface IProductAssetCategoryRepository : IRepository<ProductAssetCategory, Guid>
    {
    }
}