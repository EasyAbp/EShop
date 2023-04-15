using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Orders.Orders;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Plugins.Promotions;

public class PromotionOrderDiscountProvider : IOrderDiscountProvider, ITransientDependency
{
    public static int PromotionOrderDiscountEffectOrder { get; set; } = 5000;

    public int EffectOrder => PromotionOrderDiscountEffectOrder;

    protected IPromotionIntegrationService PromotionIntegrationService { get; }

    public PromotionOrderDiscountProvider(IPromotionIntegrationService promotionIntegrationService)
    {
        PromotionIntegrationService = promotionIntegrationService;
    }

    public virtual async Task DiscountAsync(OrderDiscountContext context)
    {
        var dto = await PromotionIntegrationService.DiscountOrderAsync(new DiscountOrderInputDto(context));

        if (dto.Context.Equals(context))
        {
            return;
        }

        context.CandidateDiscounts.Clear();
        context.CandidateDiscounts.AddRange(dto.Context.CandidateDiscounts);
    }
}