using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public static class SupplierEfCoreQueryableExtensions
    {
        public static IQueryable<Supplier> IncludeDetails(this IQueryable<Supplier> queryable, bool include = true)
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