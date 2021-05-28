using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Outstocks
{
    public static class OutstockEfCoreQueryableExtensions
    {
        public static IQueryable<Outstock> IncludeDetails(this IQueryable<Outstock> queryable, bool include = true)
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