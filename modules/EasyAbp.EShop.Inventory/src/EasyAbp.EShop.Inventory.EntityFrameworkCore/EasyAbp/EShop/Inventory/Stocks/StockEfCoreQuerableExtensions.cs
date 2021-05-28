using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Stocks
{
    public static class StockEfCoreQueryableExtensions
    {
        public static IQueryable<Stock> IncludeDetails(this IQueryable<Stock> queryable, bool include = true)
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