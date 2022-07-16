using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public static class ProductAssetEfCoreQueryableExtensions
    {
        public static IQueryable<ProductAsset> IncludeDetails(this IQueryable<ProductAsset> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(x => x.Periods);
        }
    }
}