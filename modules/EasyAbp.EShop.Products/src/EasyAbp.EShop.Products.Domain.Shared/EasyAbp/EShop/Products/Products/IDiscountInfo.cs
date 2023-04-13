using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public interface IDiscountInfo
{
    /// <summary>
    /// If you set this value, only one Discount in the same EffectGroup will be applied.
    /// For OrderDiscounts, each OrderLine can only be affected by one discount with the same EffectGroup.
    /// For ProductDiscounts, the Discount with the highest discounted amount will be applied.
    /// </summary>
    [CanBeNull]
    string EffectGroup { get; }

    [NotNull]
    string Name { get; }

    [CanBeNull]
    string Key { get; }

    [CanBeNull]
    string DisplayName { get; }
}