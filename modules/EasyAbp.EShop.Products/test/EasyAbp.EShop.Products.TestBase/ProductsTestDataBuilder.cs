using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace EasyAbp.EShop.Products
{
    public class ProductsTestDataBuilder : ITransientDependency
    {
        private readonly IProductDetailRepository _productDetailRepository;

        public ProductsTestDataBuilder(IProductDetailRepository productDetailRepository)
        {
            _productDetailRepository = productDetailRepository;
        }

        public void Build()
        {
            AsyncHelper.RunSync(BuildAsync);
        }

        public async Task BuildAsync()
        {
            await _productDetailRepository.InsertAsync(new ProductDetail(ProductsTestData.ProductDetails1Id, "Product Details"));
        }
    }
}