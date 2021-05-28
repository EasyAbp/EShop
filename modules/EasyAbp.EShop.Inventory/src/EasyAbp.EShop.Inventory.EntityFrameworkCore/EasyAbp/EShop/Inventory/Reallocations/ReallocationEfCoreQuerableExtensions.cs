using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Reallocations
{
    public static class ReallocationEfCoreQueryableExtensions
    {
        public static IQueryable<Reallocation> IncludeDetails(this IQueryable<Reallocation> queryable, bool include = true)
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