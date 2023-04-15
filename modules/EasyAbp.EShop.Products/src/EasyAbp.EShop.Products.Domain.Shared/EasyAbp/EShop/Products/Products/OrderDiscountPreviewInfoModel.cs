using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class OrderDiscountPreviewInfoModel : DiscountInfoModel, ICloneable
{
    /// <summary>
    /// This property can be used as an additional rule explanation for the UI.
    /// For example, <c>2,rate,0.1</c> could mean 10% off while the quantity of order line >= 2.
    /// UI cannot always calculate the discount amount correctly, but if you need to, this property can help.
    /// </summary>
    [CanBeNull]
    public string RuleData { get; set; }

    public OrderDiscountPreviewInfoModel()
    {
    }

    public OrderDiscountPreviewInfoModel([CanBeNull] string effectGroup, [NotNull] string name, [CanBeNull] string key,
        [CanBeNull] string displayName, DateTime? fromTime, DateTime? toTime, [CanBeNull] string ruleData) : base(
        effectGroup, name, key, displayName, fromTime, toTime)
    {
        RuleData = ruleData;
    }

    public virtual object Clone()
    {
        return new OrderDiscountPreviewInfoModel(EffectGroup, Name, Key, DisplayName, FromTime, ToTime, RuleData);
    }

    public override bool Equals(object obj)
    {
        return obj is OrderDiscountPreviewInfoModel other && Equals(other);
    }

    private bool Equals(OrderDiscountPreviewInfoModel other)
    {
        return EffectGroup == other.EffectGroup &&
               Name == other.Name &&
               Key == other.Key &&
               DisplayName == other.DisplayName &&
               Nullable.Equals(FromTime, other.FromTime) &&
               Nullable.Equals(ToTime, other.ToTime) &&
               RuleData == other.RuleData;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ (EffectGroup != null ? EffectGroup.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Key != null ? Key.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (DisplayName != null ? DisplayName.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ FromTime.GetHashCode();
            hashCode = (hashCode * 397) ^ ToTime.GetHashCode();
            hashCode = (hashCode * 397) ^ (RuleData != null ? RuleData.GetHashCode() : 0);
            return hashCode;
        }
    }
}