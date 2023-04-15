using System;
using NodaMoney;

namespace EasyAbp.EShop.Products.Products;

public static class HasDynamicDiscountAmountExtensions
{
    /// <summary>
    /// Calculate the real-time discount amount based on the current price.
    /// The calculated discount amount has been formatted with NodaMoney and MidpointRounding.ToZero.
    /// </summary>
    public static decimal CalculateDiscountAmount(this IHasDynamicDiscountAmount value, decimal currentPrice,
        string currency)
    {
        var money = new Money(
            value.DynamicDiscountAmount.DiscountAmount > decimal.Zero
                ? value.DynamicDiscountAmount.DiscountAmount
                : value.DynamicDiscountAmount.DiscountRate * currentPrice, currency, MidpointRounding.ToZero);

        if (money.Amount < decimal.Zero)
        {
            throw new DiscountAmountOverflowException();
        }

        if (money.Amount > currentPrice)
        {
            money = new Money(currentPrice, currency);
        }

        return money.Amount;
    }
}