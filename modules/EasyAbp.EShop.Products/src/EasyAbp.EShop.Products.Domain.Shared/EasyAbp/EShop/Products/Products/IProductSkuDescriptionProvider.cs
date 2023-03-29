using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSkuDescriptionProvider
    {
        Task<string> GenerateAsync(IProduct product, IProductSku productSku);
    }
}