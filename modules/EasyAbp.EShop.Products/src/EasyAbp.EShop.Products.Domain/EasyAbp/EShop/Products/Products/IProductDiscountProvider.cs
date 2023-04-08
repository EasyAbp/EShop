using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IProductDiscountProvider
{
    Task DiscountAsync(ProductDiscountContext context);
}