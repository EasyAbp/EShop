using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Instocks
{
    public static class InstockEfCoreQueryableExtensions
    {
        public static IQueryable<Instock> IncludeDetails(this IQueryable<Instock> queryable, bool include = true)
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