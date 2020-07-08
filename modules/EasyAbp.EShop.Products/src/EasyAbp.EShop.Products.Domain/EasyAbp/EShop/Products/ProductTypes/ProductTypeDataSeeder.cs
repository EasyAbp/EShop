using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public class ProductTypeDataSeeder : IProductTypeDataSeeder, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IProductTypeRepository _productTypeRepository;

        public ProductTypeDataSeeder(
            IGuidGenerator guidGenerator,
            IProductTypeRepository productTypeRepository)
        {
            _guidGenerator = guidGenerator;
            _productTypeRepository = productTypeRepository;
        }
        
        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _productTypeRepository.GetCountAsync() == 0)
            {
                await _productTypeRepository.InsertAsync(new ProductType(_guidGenerator.Create(), ProductsConsts.DefaultProductType, "Default",
                    null, MultiTenancySides.Both), true);
            }
        }
    }
}