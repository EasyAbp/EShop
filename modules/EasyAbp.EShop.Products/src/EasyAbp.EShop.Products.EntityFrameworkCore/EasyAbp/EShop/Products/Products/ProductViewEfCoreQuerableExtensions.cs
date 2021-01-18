using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.Products
{
    public static class ProductViewEfCoreQueryableExtensions
    {
        public static IQueryable<ProductView> IncludeDetails(this IQueryable<ProductView> queryable, bool include = true)
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