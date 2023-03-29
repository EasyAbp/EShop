using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public interface IHasProductGroupDisplayName
{
    [NotNull]
    string ProductGroupDisplayName { get; }
}