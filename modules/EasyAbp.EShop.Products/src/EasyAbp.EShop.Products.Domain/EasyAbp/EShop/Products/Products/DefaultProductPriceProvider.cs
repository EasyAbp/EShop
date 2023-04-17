using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products
{
    public class DefaultProductPriceProvider : IProductPriceProvider, ITransientDependency
    {
        public virtual Task<List<ProductRealTimePriceInfoModel>> GetPricesAsync(
            IEnumerable<ProductAndSkuDataModel> models)
        {
            return Task.FromResult(models.Select(x =>
                new ProductRealTimePriceInfoModel(x.Product.Id, x.ProductSku.Id, x.ProductSku.Price)).ToList());
        }
    }
}