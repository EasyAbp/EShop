using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductTypes;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Products
{
    public class ProductsDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IProductTypeDataSeeder _productTypeDataSeeder;

        public ProductsDataSeedContributor(IProductTypeDataSeeder productTypeDataSeeder)
        {
            _productTypeDataSeeder = productTypeDataSeeder;
        }
        
        [UnitOfWork(true)]
        public async Task SeedAsync(DataSeedContext context)
        {
            await _productTypeDataSeeder.SeedAsync(context);
        }
    }
}