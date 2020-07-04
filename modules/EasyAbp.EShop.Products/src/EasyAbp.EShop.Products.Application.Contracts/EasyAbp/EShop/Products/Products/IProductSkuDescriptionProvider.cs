using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products.Dtos;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductSkuDescriptionProvider
    {
        Task<string> GenerateAsync(ProductDto productDto, ProductSkuDto productSkuDto);
    }
}