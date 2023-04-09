using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class ProductDiscountInfoModel : DiscountInfoModel, ICloneable
{
    public decimal DiscountedAmount { get; set; }

    public ProductDiscountInfoModel()
    {
    }

    public ProductDiscountInfoModel([NotNull] string name, [CanBeNull] string key, [CanBeNull] string displayName,
        decimal discountedAmount, DateTime? fromTime, DateTime? toTime) : base(name, key, displayName, fromTime, toTime)
    {
        if (discountedAmount < decimal.Zero)
        {
            throw new DiscountAmountOverflowException();
        }

        DiscountedAmount = discountedAmount;
    }

    public object Clone()
    {
        return new ProductDiscountInfoModel(Name, Key, DisplayName, DiscountedAmount, FromTime, ToTime);
    }

    public override bool Equals(object obj)
    {
        return obj is ProductDiscountInfoModel other && Equals(other);
    }

    protected bool Equals(ProductDiscountInfoModel other)
    {
        return Name == other.Name &&
               Key == other.Key &&
               DisplayName == other.DisplayName &&
               DiscountedAmount == other.DiscountedAmount &&
               Nullable.Equals(FromTime, other.FromTime) &&
               Nullable.Equals(ToTime, other.ToTime);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ DiscountedAmount.GetHashCode();
            hashCode = (hashCode * 397) ^ FromTime.GetHashCode();
            hashCode = (hashCode * 397) ^ ToTime.GetHashCode();
            return hashCode;
        }
    }
}