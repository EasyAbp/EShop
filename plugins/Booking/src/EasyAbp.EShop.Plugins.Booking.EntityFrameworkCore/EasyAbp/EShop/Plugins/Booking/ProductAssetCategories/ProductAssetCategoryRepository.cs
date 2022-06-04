using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<ProductAssetCategory> FindEffectiveAsync(DateTime currentTime, Guid storeId,
            Guid productId, Guid productSkuId, Guid assetCategoryId, Guid periodSchemeId)
        {
            return await (await GetQueryableAsync())
                .Where(x =>
                    x.StoreId == storeId &&
                    x.ProductId == productId &&
                    x.ProductSkuId == productSkuId &&
                    x.AssetCategoryId == assetCategoryId &&
                    x.PeriodSchemeId == periodSchemeId
                    && x.FromTime <= currentTime &&
                    (!x.ToTime.HasValue || x.ToTime.Value > currentTime))
                .OrderByDescending(x => x.FromTime)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<bool> ExistConflictAsync(Guid id, Guid storeId, Guid productId, Guid productSkuId,
            Guid assetCategoryId, Guid periodSchemeId, DateTime fromTime)
        {
            var conflict = await (await GetQueryableAsync())
                .Where(x =>
                    x.StoreId == storeId &&
                    x.ProductId == productId &&
                    x.ProductSkuId == productSkuId &&
                    x.AssetCategoryId == assetCategoryId &&
                    x.PeriodSchemeId == periodSchemeId
                    && x.FromTime == fromTime)
                .FirstOrDefaultAsync();
            
            return conflict is not null && conflict.Id != id;
        }
    }
}
