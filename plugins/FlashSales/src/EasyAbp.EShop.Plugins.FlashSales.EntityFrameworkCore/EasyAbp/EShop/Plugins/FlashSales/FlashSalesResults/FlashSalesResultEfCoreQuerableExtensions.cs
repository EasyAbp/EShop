using System.Linq;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public static class FlashSalesResultEfCoreQuerableExtensions
{
    public static IQueryable<FlashSalesResult> IncludeDetails(this IQueryable<FlashSalesResult> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable;
    }
}