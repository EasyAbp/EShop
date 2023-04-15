using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public static class PromotionEfCoreQueryableExtensions
{
    public static IQueryable<Promotion> IncludeDetails(this IQueryable<Promotion> queryable, bool include = true)
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
