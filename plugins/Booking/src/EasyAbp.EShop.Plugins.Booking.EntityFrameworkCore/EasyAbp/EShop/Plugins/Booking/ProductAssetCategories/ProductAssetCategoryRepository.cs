using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssetCategories
{
    public class ProductAssetCategoryRepository : EfCoreRepository<IBookingDbContext, ProductAssetCategory, Guid>, IProductAssetCategoryRepository
    {
        public ProductAssetCategoryRepository(IDbContextProvider<IBookingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<ProductAssetCategory>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
