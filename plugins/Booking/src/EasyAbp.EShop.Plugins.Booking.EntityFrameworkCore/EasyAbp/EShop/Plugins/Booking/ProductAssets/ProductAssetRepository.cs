using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public class ProductAssetRepository : EfCoreRepository<IBookingDbContext, ProductAsset, Guid>, IProductAssetRepository
    {
        public ProductAssetRepository(IDbContextProvider<IBookingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<ProductAsset>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
