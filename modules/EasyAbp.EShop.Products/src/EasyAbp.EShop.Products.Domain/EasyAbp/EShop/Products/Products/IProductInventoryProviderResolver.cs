using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public interface IProductInventoryProviderResolver
{
    Task<bool> ExistProviderAsync([NotNull] string providerName);

    Task<IProductInventoryProvider> GetAsync(Product product);

    Task<IProductInventoryProvider> GetAsync([NotNull] string providerName);
}