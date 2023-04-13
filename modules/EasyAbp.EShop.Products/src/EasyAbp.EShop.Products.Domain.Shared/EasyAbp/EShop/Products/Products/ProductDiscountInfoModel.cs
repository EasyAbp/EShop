using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class ProductDiscountInfoModel : DiscountInfoModel, ICloneable
{
    public decimal DiscountedAmount { get; set; }

    public bool? InEffect { get; set; }

    public ProductDiscountInfoModel()
    {
    }

    public ProductDiscountInfoModel([CanBeNull] string effectGroup, [NotNull] string name, [CanBeNull] string key,
        [CanBeNull] string displayName, decimal discountedAmount, DateTime? fromTime, DateTime? toTime,
        bool? inEffect = null) : base(effectGroup, name, key, displayName, fromTime, toTime)
    {
        if (discountedAmount < decimal.Zero)
        {
            throw new DiscountAmountOverflowException();
        }

        DiscountedAmount = discountedAmount;
        InEffect = inEffect;
    }

    public virtual object Clone()
    {
        return new ProductDiscountInfoModel(
            EffectGroup, Name, Key, DisplayName, DiscountedAmount, FromTime, ToTime, InEffect);
    }

    public override bool Equals(object obj)
    {
        return obj is ProductDiscountInfoModel other && Equals(other);
    }

    private bool Equals(ProductDiscountInfoModel other)
    {
        return EffectGroup == other.EffectGroup &&
               Name == other.Name &&
               Key == other.Key &&
               DisplayName == other.DisplayName &&
               DiscountedAmount == other.DiscountedAmount &&
               Nullable.Equals(InEffect, other.InEffect) &&
               Nullable.Equals(FromTime, other.FromTime) &&
               Nullable.Equals(ToTime, other.ToTime);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (EffectGroup != null ? EffectGroup.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ DiscountedAmount.GetHashCode();
            hashCode = (hashCode * 397) ^ InEffect.GetHashCode();
            hashCode = (hashCode * 397) ^ FromTime.GetHashCode();
            hashCode = (hashCode * 397) ^ ToTime.GetHashCode();
            return hashCode;
        }
    }
}