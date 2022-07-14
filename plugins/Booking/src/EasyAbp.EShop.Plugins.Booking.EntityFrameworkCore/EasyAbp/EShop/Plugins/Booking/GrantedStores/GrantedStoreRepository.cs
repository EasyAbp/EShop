using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public class GrantedStoreRepository : EfCoreRepository<IBookingDbContext, GrantedStore, Guid>, IGrantedStoreRepository
    {
        public GrantedStoreRepository(IDbContextProvider<IBookingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<GrantedStore>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
