using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductPriceProvider
    {
        Task<List<ProductRealTimePriceInfoModel>> GetPricesAsync(IEnumerable<ProductAndSkuDataModel> models);
    }
}