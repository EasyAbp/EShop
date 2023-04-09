using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class OrderDiscountPreviewInfoModel : DiscountInfoModel, ICloneable
{
    public decimal MinDiscountedAmount { get; set; }

    public decimal MaxDiscountedAmount { get; set; }

    public OrderDiscountPreviewInfoModel()
    {
    }

    public OrderDiscountPreviewInfoModel([NotNull] string name, [CanBeNull] string key, [CanBeNull] string displayName,
        decimal minDiscountedAmount, decimal maxDiscountedAmount, DateTime? fromTime, DateTime? toTime) : base(name,
        key, displayName, fromTime, toTime)
    {
        if (minDiscountedAmount < decimal.Zero || maxDiscountedAmount < decimal.Zero ||
            minDiscountedAmount > maxDiscountedAmount)
        {
            throw new DiscountAmountOverflowException();
        }

        MinDiscountedAmount = minDiscountedAmount;
        MaxDiscountedAmount = maxDiscountedAmount;
    }

    public object Clone()
    {
        return new OrderDiscountPreviewInfoModel(Name, Key, DisplayName, MinDiscountedAmount, MaxDiscountedAmount,
            FromTime, ToTime);
    }

    public override bool Equals(object obj)
    {
        return obj is OrderDiscountPreviewInfoModel other && Equals(other);
    }

    protected bool Equals(OrderDiscountPreviewInfoModel other)
    {
        return Name == other.Name &&
               Key == other.Key &&
               DisplayName == other.DisplayName &&
               MinDiscountedAmount == other.MinDiscountedAmount &&
               MaxDiscountedAmount == other.MaxDiscountedAmount &&
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
            hashCode = (hashCode * 397) ^ MinDiscountedAmount.GetHashCode();
            hashCode = (hashCode * 397) ^ MaxDiscountedAmount.GetHashCode();
            hashCode = (hashCode * 397) ^ FromTime.GetHashCode();
            hashCode = (hashCode * 397) ^ ToTime.GetHashCode();
            return hashCode;
        }
    }
}