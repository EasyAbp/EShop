using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

public interface IDiscountInfo
{
    /// <summary>
    /// If you set this value, only one Discount in the same EffectGroup will be applied.
    /// For OrderDiscounts, each OrderLine can only be affected by one discount with the same EffectGroup.
    /// For ProductDiscounts, the Discount with the highest discount amount will be applied.
    /// </summary>
    [CanBeNull]
    string EffectGroup { get; }

    /// <summary>
    /// If there is more than one Discount with the same <see cref="Name"/> and <see cref="Key"/>,
    /// only the one with the highest discount amount will be applied.
    /// </summary>
    [NotNull]
    string Name { get; }

    /// <summary>
    /// If there is more than one Discount with the same <see cref="Name"/> and <see cref="Key"/>,
    /// only the one with the highest discount amount will be applied.
    /// </summary>
    [CanBeNull]
    string Key { get; }

    [CanBeNull]
    string DisplayName { get; }
}