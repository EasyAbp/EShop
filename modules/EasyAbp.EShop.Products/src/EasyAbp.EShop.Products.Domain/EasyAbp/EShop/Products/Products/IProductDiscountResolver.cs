using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IProductDiscountResolver
{
    Task ResolveAsync(GetProductsRealTimePriceContext context);
}