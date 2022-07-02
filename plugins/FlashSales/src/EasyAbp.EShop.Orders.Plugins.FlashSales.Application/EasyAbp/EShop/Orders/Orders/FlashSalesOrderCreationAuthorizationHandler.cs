using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans.Dtos;
using EasyAbp.EShop.Plugins.FlashSales.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class FlashSalesOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
{
    protected IAbpLazyServiceProvider LazyServiceProvider { get; }
    protected IStringLocalizer<FlashSalesResource> Localizer { get; }

    public FlashSalesOrderCreationAuthorizationHandler(
        IAbpLazyServiceProvider lazyServiceProvider,
        IStringLocalizer<FlashSalesResource> localizer)
    {
        LazyServiceProvider = lazyServiceProvider;
        Localizer = localizer;
    }

    protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
    {
        if (await IsFlashSalesPlanProductSkuAsync(resource))
        {
            context.Fail(new AuthorizationFailureReason(this, Localizer["ExistFlashSalesPlanProduct"]));
            return;
        }

        context.Succeed(requirement);
    }

    protected virtual async Task<bool> IsFlashSalesPlanProductSkuAsync(OrderCreationResource resource)
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
                return true;
            }
        }

        return false;
    }
}
