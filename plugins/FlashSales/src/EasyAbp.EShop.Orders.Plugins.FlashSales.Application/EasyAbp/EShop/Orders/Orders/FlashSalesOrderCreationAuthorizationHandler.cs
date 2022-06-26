using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class FlashSalesOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
{
    protected IAbpLazyServiceProvider LazyServiceProvider { get; }

    public FlashSalesOrderCreationAuthorizationHandler(IAbpLazyServiceProvider lazyServiceProvider)
    {
        LazyServiceProvider = lazyServiceProvider;
    }

    protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
    {
        var flashSalesPlanAppService = LazyServiceProvider.LazyGetRequiredService<IFlashSalesPlanAppService>();
        foreach (var orderLine in resource.Input.OrderLines)
        {
            var plans = await flashSalesPlanAppService.GetListAsync(new FlashSalesPlanGetListInput()
            {
                StoreId = resource.Input.StoreId,
                ProductId = orderLine.ProductId,
                ProductSkuId = orderLine.ProductSkuId,
                OnlyShowPublished = true
            });
            if (plans.Items.Count > 0)
            {
                context.Fail();
                return;
            }
        }

        context.Succeed(requirement);
    }
}
