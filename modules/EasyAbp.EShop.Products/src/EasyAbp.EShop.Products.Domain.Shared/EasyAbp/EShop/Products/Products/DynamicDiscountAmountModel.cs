using System;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.Products;

[Serializable]
public class DynamicDiscountAmountModel
{
    public string Currency { get; }

    /// <summary>
    /// The absolute discount amount.
    /// <see cref="DiscountAmount"/> has a higher priority of effectiveness than <see cref="DiscountRate"/>.
    /// And <see cref="DiscountRate"/> has a higher priority of effectiveness than <see cref="CalculatorName"/>.
    /// </summary>
    public decimal DiscountAmount { get; }

    /// <summary>
    /// The discount rate. This means 10% off if you set it to <value>0.1</value>.
    /// <see cref="DiscountAmount"/> has a higher priority of effectiveness than <see cref="DiscountRate"/>.
    /// And <see cref="DiscountRate"/> has a higher priority of effectiveness than <see cref="CalculatorName"/>.
    /// </summary>
    public decimal DiscountRate { get; }

    /// <summary>
    /// (todo: NOT YET IMPLEMENTED!)
    /// The name of the runtime discount calculator.
    /// <see cref="DiscountAmount"/> has a higher priority of effectiveness than <see cref="DiscountRate"/>.
    /// And <see cref="DiscountRate"/> has a higher priority of effectiveness than <see cref="CalculatorName"/>.
    /// </summary>
    [CanBeNull]
    public string CalculatorName { get; }

    public DynamicDiscountAmountModel(string currency, decimal discountAmount, decimal discountRate,
        [CanBeNull] string calculatorName)
    {
        if (discountAmount < decimal.Zero || discountRate < decimal.Zero)
        {
            throw new DiscountAmountOverflowException();
        }

        Currency = currency;
        DiscountAmount = discountAmount;
        DiscountRate = discountRate;
        CalculatorName = calculatorName;
    }
}