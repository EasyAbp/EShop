using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Products;

public interface IProductDiscountProvider
{
    int EffectOrder { get; }

    Task DiscountAsync(ProductDiscountContext context);
}