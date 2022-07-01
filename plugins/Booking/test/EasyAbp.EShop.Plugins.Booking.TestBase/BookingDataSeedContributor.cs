using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Booking;

public class BookingDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IGrantedStoreRepository _grantedStoreRepository;
    private readonly ProductAssetManager _productAssetManager;
    private readonly IProductAssetRepository _productAssetRepository;
    private readonly ProductAssetCategoryManager _productAssetCategoryManager;
    private readonly IProductAssetCategoryRepository _productAssetCategoryRepository;

    public BookingDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IGrantedStoreRepository grantedStoreRepository,
        ProductAssetManager productAssetManager,
        IProductAssetRepository productAssetRepository,
        ProductAssetCategoryManager productAssetCategoryManager,
        IProductAssetCategoryRepository productAssetCategoryRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _grantedStoreRepository = grantedStoreRepository;
        _productAssetManager = productAssetManager;
        _productAssetRepository = productAssetRepository;
        _productAssetCategoryManager = productAssetCategoryManager;
        _productAssetCategoryRepository = productAssetCategoryRepository;
    }

    [UnitOfWork]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using var change = _currentTenant.Change(context?.TenantId);

        await _grantedStoreRepository.InsertAsync(new GrantedStore(_guidGenerator.Create(), _currentTenant.Id,
            BookingTestConsts.Store1Id, BookingTestConsts.Asset1Id, null, false));

        await _grantedStoreRepository.InsertAsync(new GrantedStore(_guidGenerator.Create(), _currentTenant.Id,
            BookingTestConsts.Store1Id, null, BookingTestConsts.AssetCategory1Id, false));

        await _productAssetRepository.InsertAsync(await _productAssetManager.CreateAsync(BookingTestConsts.Store1Id,
            BookingTestConsts.BookingProduct1Id, BookingTestConsts.BookingProduct1Sku1Id, BookingTestConsts.Asset1Id,
            BookingTestConsts.PeriodScheme1Id, DateTime.Parse("1970-1-1"), null, 5m));

        await _productAssetCategoryRepository.InsertAsync(await _productAssetCategoryManager.CreateAsync(
            BookingTestConsts.Store1Id, BookingTestConsts.BookingProduct1Id, BookingTestConsts.BookingProduct1Sku1Id,
            BookingTestConsts.AssetCategory1Id, BookingTestConsts.PeriodScheme1Id, DateTime.Parse("1970-1-1"), null,
            10m));
    }
}