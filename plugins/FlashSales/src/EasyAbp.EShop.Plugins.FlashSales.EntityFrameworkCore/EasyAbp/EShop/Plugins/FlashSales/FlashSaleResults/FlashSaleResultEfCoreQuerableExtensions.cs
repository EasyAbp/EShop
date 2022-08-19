using System.Linq;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public static class FlashSaleResultEfCoreQuerableExtensions
{
    public static IQueryable<FlashSaleResult> IncludeDetails(this IQueryable<FlashSaleResult> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable;
    }
}