using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public static class CouponTemplateEfCoreQueryableExtensions
    {
        public static IQueryable<CouponTemplate> IncludeDetails(this IQueryable<CouponTemplate> queryable, bool include = true)
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