using System.Linq;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;

public static class FlashSalesPlanEfCoreQueryableExtensions
{
    public static IQueryable<FlashSalesPlan> IncludeDetails(this IQueryable<FlashSalesPlan> queryable, bool include = true)
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