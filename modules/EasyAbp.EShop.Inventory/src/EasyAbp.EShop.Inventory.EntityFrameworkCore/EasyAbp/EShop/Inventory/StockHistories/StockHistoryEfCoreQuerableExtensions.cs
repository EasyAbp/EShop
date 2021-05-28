using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.StockHistories
{
    public static class StockHistoryEfCoreQueryableExtensions
    {
        public static IQueryable<StockHistory> IncludeDetails(this IQueryable<StockHistory> queryable, bool include = true)
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