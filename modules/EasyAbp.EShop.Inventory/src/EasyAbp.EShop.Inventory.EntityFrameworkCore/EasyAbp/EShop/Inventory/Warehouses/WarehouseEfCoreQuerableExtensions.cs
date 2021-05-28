using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public static class WarehouseEfCoreQueryableExtensions
    {
        public static IQueryable<Warehouse> IncludeDetails(this IQueryable<Warehouse> queryable, bool include = true)
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