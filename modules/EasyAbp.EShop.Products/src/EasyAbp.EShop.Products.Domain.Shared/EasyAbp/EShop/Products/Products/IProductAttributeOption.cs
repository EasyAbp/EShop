using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttributeOption : IHasExtraProperties
    {
        [NotNull]
        string DisplayName { get; }

        [CanBeNull]
        string Description { get; }

        int DisplayOrder { get; }
    }
}