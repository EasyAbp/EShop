using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public static class GrantedStoreEfCoreQueryableExtensions
    {
        public static IQueryable<GrantedStore> IncludeDetails(this IQueryable<GrantedStore> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable;
        }
    }
}