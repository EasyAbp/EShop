using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Stores.Settings;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace EShopSample.Data;

public class SampleDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly IStoreRepository _storeRepository;
    private readonly IProductManager _productManager;
    private readonly IProductRepository _productRepository;
    private readonly ICategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IProductCategoryRepository _productCategoryRepository;
    private readonly ISettingProvider _settingProvider;
    private readonly IAttributeOptionIdsSerializer _attributeOptionIdsSerializer;

    public SampleDataSeedContributor(
        IGuidGenerator guidGenerator,
        ICurrentTenant currentTenant,
        IStoreRepository storeRepository,
        IProductManager productManager,
        IProductRepository productRepository,
        ICategoryManager categoryManager,
        ICategoryRepository categoryRepository,
        IProductCategoryRepository productCategoryRepository,
        ISettingProvider settingProvider,
        IAttributeOptionIdsSerializer attributeOptionIdsSerializer)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        _storeRepository = storeRepository;
        _productManager = productManager;
        _productRepository = productRepository;
        _categoryManager = categoryManager;
        _categoryRepository = categoryRepository;
        _productCategoryRepository = productCategoryRepository;
        _settingProvider = settingProvider;
        _attributeOptionIdsSerializer = attributeOptionIdsSerializer;
    }

    [UnitOfWork(true)]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using var changeTenant = _currentTenant.Change(context.TenantId);

        await SeedStoresAsync();
        await SeedCategoriesAsync();
        await SeedProductsAsync();
        await SeedProductCategoryMappingsAsync();
    }

    protected virtual async Task SeedProductCategoryMappingsAsync()
    {
        var product = await _productRepository.GetAsync(
            x => x.UniqueName == SampleDataConsts.CakeProductUniqueName);

        var category = await _categoryRepository.GetAsync(
            x => x.UniqueName == SampleDataConsts.FoodsCategoryUniqueName);

        if (await _productCategoryRepository.AnyAsync(x => x.ProductId == product.Id && x.CategoryId == category.Id))
        {
            return;
        }

        await _productCategoryRepository.InsertAsync(
            new ProductCategory(_guidGenerator.Create(), _currentTenant.Id, category.Id, product.Id));
    }

    public virtual async Task SeedStoresAsync()
    {
        var storeName = await _settingProvider.GetOrNullAsync(StoresSettings.DefaultStoreName);

        if (await _storeRepository.AnyAsync(x => x.Name == storeName))
        {
            return;
        }

        await _storeRepository.InsertAsync(new Store(_guidGenerator.Create(), _currentTenant.Id, storeName), true);
    }

    public virtual async Task SeedCategoriesAsync()
    {
        if (await _categoryRepository.AnyAsync(x => x.UniqueName == SampleDataConsts.FoodsCategoryUniqueName))
        {
            return;
        }

        var category = await _categoryManager.CreateAsync(null, SampleDataConsts.FoodsCategoryUniqueName, "Foods",
            "Some delicious foods.", null, false);

        await _categoryRepository.InsertAsync(category, true);
    }

    public virtual async Task SeedProductsAsync()
    {
        if (await _productRepository.AnyAsync(x => x.UniqueName == SampleDataConsts.CakeProductUniqueName))
        {
            return;
        }

        var defaultStore = await _storeRepository.FindDefaultStoreAsync();

        var product = new Product(
            _guidGenerator.Create(),
            _currentTenant.Id,
            defaultStore.Id,
            ProductsConsts.DefaultProductGroupName,
            null,
            SampleDataConsts.CakeProductUniqueName,
            "Cake",
            "Delicious cakes",
            InventoryStrategy.NoNeed,
            null,
            true,
            false,
            false,
            TimeSpan.FromMinutes(15),
            null,
            0);

        var attribute1 = new ProductAttribute(_guidGenerator.Create(), "Size", null, 2);
        var attribute2 = new ProductAttribute(_guidGenerator.Create(), "Flavor", null, 1);

        attribute1.ProductAttributeOptions.AddRange(new[]
        {
            new ProductAttributeOption(_guidGenerator.Create(), "S", null, 1),
            new ProductAttributeOption(_guidGenerator.Create(), "M", null, 2),
            new ProductAttributeOption(_guidGenerator.Create(), "L", null, 3),
        });

        attribute2.ProductAttributeOptions.AddRange(new[]
        {
            new ProductAttributeOption(_guidGenerator.Create(), "Chocolate", null, 1),
            new ProductAttributeOption(_guidGenerator.Create(), "Vanilla", null, 2),
        });

        product.ProductAttributes.Add(attribute1);
        product.ProductAttributes.Add(attribute2);

        await _productManager.CreateAsync(product);

        var productSku1 = new ProductSku(_guidGenerator.Create(),
            await _attributeOptionIdsSerializer.SerializeAsync(new[]
                { attribute1.ProductAttributeOptions[0].Id, attribute2.ProductAttributeOptions[0].Id }),
            null, "USD", null, 1m, 1, 10, null, null, null);

        var productSku2 = new ProductSku(_guidGenerator.Create(),
            await _attributeOptionIdsSerializer.SerializeAsync(new[]
                { attribute1.ProductAttributeOptions[1].Id, attribute2.ProductAttributeOptions[0].Id }),
            null, "USD", null, 2m, 1, 10, null, null, null);

        var productSku3 = new ProductSku(_guidGenerator.Create(),
            await _attributeOptionIdsSerializer.SerializeAsync(new[]
                { attribute1.ProductAttributeOptions[1].Id, attribute2.ProductAttributeOptions[1].Id }),
            null, "USD", null, 3m, 1, 10, null, null, null);

        var productSku4 = new ProductSku(_guidGenerator.Create(),
            await _attributeOptionIdsSerializer.SerializeAsync(new[]
                { attribute1.ProductAttributeOptions[2].Id, attribute2.ProductAttributeOptions[1].Id }),
            null, "USD", null, 4m, 1, 10, null, null, null);

        await _productManager.CreateSkuAsync(product, productSku1);
        await _productManager.CreateSkuAsync(product, productSku2);
        await _productManager.CreateSkuAsync(product, productSku3);
        await _productManager.CreateSkuAsync(product, productSku4);
    }
}