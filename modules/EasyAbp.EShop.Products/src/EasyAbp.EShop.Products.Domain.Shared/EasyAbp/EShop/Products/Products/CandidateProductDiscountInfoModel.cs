using System;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class CandidateProductDiscountInfoModel : DiscountInfoModel, IHasDynamicDiscountAmount
{
    public DynamicDiscountAmountModel DynamicDiscountAmount { get; set; }

    public CandidateProductDiscountInfoModel()
    {
    }

    public CandidateProductDiscountInfoModel([CanBeNull] string effectGroup, [NotNull] string name,
        [CanBeNull] string key, [CanBeNull] string displayName, DynamicDiscountAmountModel dynamicDiscountAmount,
        DateTime? fromTime, DateTime? toTime) : base(effectGroup, name, key, displayName, fromTime, toTime)
    {
        DynamicDiscountAmount = Check.NotNull(dynamicDiscountAmount, nameof(dynamicDiscountAmount));
    }
}