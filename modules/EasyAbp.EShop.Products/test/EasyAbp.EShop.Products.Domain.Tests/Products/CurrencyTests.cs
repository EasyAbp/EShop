using System;
using System.Threading.Tasks;
using NodaMoney;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.Products;

public class CurrencyTests : ProductsDomainTestBase
{
    [Fact]
    public Task Should_Rounding_If_Amount_Is_Out_Of_Decimals()
    {
        var money = new Money(1.115m, Currency.FromCode("CNY"));
        
        money.Currency.ShouldBe(Currency.FromCode("CNY"));
        money.Amount.ShouldBe(1.12m);
        
        return Task.CompletedTask;
    }
    [Fact]
    public Task Should_Throw_If_Currency_Is_Undefined()
    {
        var exception = Should.Throw<ArgumentException>(() => new Money(1.115m, Currency.FromCode("BTC")));
        
        exception.Message.ShouldBe("BTC is an unknown currency code!");
        
        return Task.CompletedTask;
    }
}