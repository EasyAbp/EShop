using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public static class StoreAssetCategoryEfCoreQueryableExtensions
    {
        public static IQueryable<StoreAssetCategory> IncludeDetails(this IQueryable<StoreAssetCategory> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }
    }
}