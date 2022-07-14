using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public static class ProductAssetCategoryEfCoreQueryableExtensions
    {
        public static IQueryable<ProductAssetCategory> IncludeDetails(this IQueryable<ProductAssetCategory> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(x => x.Periods);
        }
    }
}