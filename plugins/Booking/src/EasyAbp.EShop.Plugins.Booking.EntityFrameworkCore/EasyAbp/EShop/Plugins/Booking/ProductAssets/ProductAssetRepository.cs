using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets
{
    public class ProductAssetRepository : EfCoreRepository<IBookingDbContext, ProductAsset, Guid>,
        IProductAssetRepository
    {
        public ProductAssetRepository(IDbContextProvider<IBookingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<ProductAsset>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }

        public virtual async Task<ProductAsset> FindEffectiveAsync(DateTime currentTime, Guid storeId, Guid productId,
            Guid productSkuId, Guid assetId, Guid periodSchemeId)
        {
            return await (await GetQueryableAsync())
                .Where(x =>
                    x.StoreId == storeId &&
                    x.ProductId == productId &&
                    x.ProductSkuId == productSkuId &&
                    x.AssetId == assetId &&
                    x.PeriodSchemeId == periodSchemeId
                    && x.FromTime <= currentTime &&
                    (!x.ToTime.HasValue || x.ToTime.Value > currentTime))
                .OrderByDescending(x => x.FromTime)
                .FirstOrDefaultAsync();
        }

        public virtual async Task<bool> ExistConflictAsync(Guid id, Guid storeId, Guid productId, Guid productSkuId,
            Guid assetId, Guid periodSchemeId, DateTime fromTime)
        {
            var conflict = await (await GetQueryableAsync())
                .Where(x =>
                    x.StoreId == storeId &&
                    x.ProductId == productId &&
                    x.ProductSkuId == productSkuId &&
                    x.AssetId == assetId &&
                    x.PeriodSchemeId == periodSchemeId
                    && x.FromTime == fromTime)
                .FirstOrDefaultAsync();
            
            return conflict is not null && conflict.Id != id;
        }
    }
}