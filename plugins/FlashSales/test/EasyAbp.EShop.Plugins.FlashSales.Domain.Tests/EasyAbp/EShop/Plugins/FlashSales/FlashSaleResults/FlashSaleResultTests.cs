using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class FlashSaleResultTests
{
    [Fact]
    public void MarkAsSuccessful()
    {
        var flashSaleResult = new FlashSaleResult(
            id: Guid.NewGuid(),
            tenantId: null,
            storeId: Guid.NewGuid(),
            planId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            DateTime.Now
        );

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Pending);

        var orderId = Guid.NewGuid();
        flashSaleResult.MarkAsSuccessful(orderId);

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Successful);
        flashSaleResult.OrderId.ShouldBe(orderId);
    }

    [Fact]
    public void MarkAsSuccessful_Should_Throw_FlashSaleResultStatusNotPendingException_When_Status_Not_Pending()
    {
        var flashSaleResult = new FlashSaleResult(
            id: Guid.NewGuid(),
            tenantId: null,
            storeId: Guid.NewGuid(),
            planId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            DateTime.Now
        );

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Pending);

        var orderId = Guid.NewGuid();
        flashSaleResult.MarkAsSuccessful(orderId);
        flashSaleResult.Reason.ShouldBe(null);

        Assert.Throws<FlashSaleResultStatusNotPendingException>(() =>
        {
            flashSaleResult.MarkAsSuccessful(orderId);
        });
    }

    [Fact]
    public void MarkAsFailed()
    {
        var flashSaleResult = new FlashSaleResult(
            id: Guid.NewGuid(),
            tenantId: null,
            storeId: Guid.NewGuid(),
            planId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            DateTime.Now
        );

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Pending);

        flashSaleResult.MarkAsFailed("reason");

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Failed);
        flashSaleResult.OrderId.ShouldBe(null);
        flashSaleResult.Reason.ShouldBe("reason");
    }

    [Fact]
    public void MarkAsFailed_Should_Throw_FlashSaleResultStatusNotPendingException_When_Status_Not_Pending()
    {
        var flashSaleResult = new FlashSaleResult(
            id: Guid.NewGuid(),
            tenantId: null,
            storeId: Guid.NewGuid(),
            planId: Guid.NewGuid(),
            userId: Guid.NewGuid(),
            DateTime.Now
        );

        flashSaleResult.Status.ShouldBe(FlashSaleResultStatus.Pending);

        flashSaleResult.MarkAsFailed("reason");

        Assert.Throws<FlashSaleResultStatusNotPendingException>(() =>
        {
            flashSaleResult.MarkAsFailed("reason");
        });
    }
}
