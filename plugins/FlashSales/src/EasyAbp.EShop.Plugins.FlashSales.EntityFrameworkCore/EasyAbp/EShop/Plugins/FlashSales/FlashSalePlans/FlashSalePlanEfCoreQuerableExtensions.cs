using System.Linq;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;

public static class FlashSalePlanEfCoreQueryableExtensions
{
    public static IQueryable<FlashSalePlan> IncludeDetails(this IQueryable<FlashSalePlan> queryable, bool include = true)
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