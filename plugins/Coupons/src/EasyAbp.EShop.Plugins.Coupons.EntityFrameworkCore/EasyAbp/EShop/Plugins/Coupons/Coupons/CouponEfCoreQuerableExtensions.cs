using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public static class CouponEfCoreQueryableExtensions
    {
        public static IQueryable<Coupon> IncludeDetails(this IQueryable<Coupon> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}