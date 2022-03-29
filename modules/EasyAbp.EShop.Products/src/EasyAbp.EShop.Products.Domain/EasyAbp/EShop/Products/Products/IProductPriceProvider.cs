using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductPriceProvider
    {
        Task<decimal> GetPriceAsync(IProduct product, IProductSku productSku);
    }
}