using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    [DependsOn(
        typeof(EShopProductsDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
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
                options.AddRepository<Product, ProductRepository>();
                options.AddRepository<Category, CategoryRepository>();
                options.AddRepository<ProductType, ProductTypeRepository>();
                options.AddRepository<ProductCategory, ProductCategoryRepository>();
            });
        }
    }
}
