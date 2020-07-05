using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.Abp.Trees.EntityFrameworkCore;
using EasyAbp.EShop.Products.ProductDetailHistories;
using EasyAbp.EShop.Products.ProductHistories;
using EasyAbp.EShop.Products.ProductStores;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopProductsDomainModule),
        typeof(AbpEntityFrameworkCoreModule),
        typeof(AbpTreesEntityFrameworkCoreModule)
    )]
    public class EShopProductsEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<ProductsDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Category, CategoryRepository>();
                options.AddRepository<Product, ProductRepository>();
                options.AddRepository<ProductDetail, ProductDetailRepository>();
                options.AddRepository<ProductType, ProductTypeRepository>();
                options.AddRepository<ProductCategory, ProductCategoryRepository>();
                options.AddRepository<ProductStore, ProductStoreRepository>();
                options.AddRepository<ProductHistory, ProductHistoryRepository>();
                options.AddRepository<ProductDetailHistory, ProductDetailHistoryRepository>();
                options.AddRepository<ProductInventory, ProductInventoryRepository>();
            });
        }
    }
}
