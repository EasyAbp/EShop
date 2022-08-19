using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
public class FlashSalePlanHasherTests : FlashSalesDomainTestBase
{
    protected IFlashSalePlanHasher FlashSalePlanHasher { get; }

    public FlashSalePlanHasherTests()
    {
        FlashSalePlanHasher = GetRequiredService<FlashSalePlanHasher>();
    }

    [Fact]
    public async Task HashAsync()
    {
        var time1 = DateTime.Now;
        var time2 = DateTime.Now.AddTicks(10);
        var time3 = DateTime.Now.AddTicks(20);

        (await FlashSalePlanHasher.HashAsync(time1, time2, time3))
            .ShouldBe(await FlashSalePlanHasher.HashAsync(time1, time2, time3));
    }
}
