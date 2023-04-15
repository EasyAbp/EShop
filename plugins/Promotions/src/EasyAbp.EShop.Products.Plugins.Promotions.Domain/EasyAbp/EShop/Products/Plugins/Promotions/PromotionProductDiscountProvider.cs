using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Plugins.Promotions;

public class PromotionProductDiscountProvider : IProductDiscountProvider, ITransientDependency
{
    public static int PromotionProductDiscountEffectOrder { get; set; } = 5000;

    public int EffectOrder => PromotionProductDiscountEffectOrder;

    protected IPromotionIntegrationService PromotionIntegrationService { get; }

    public PromotionProductDiscountProvider(IPromotionIntegrationService promotionIntegrationService)
    {
        PromotionIntegrationService = promotionIntegrationService;
    }

    public virtual async Task DiscountAsync(ProductDiscountContext context)
    {
        var dto = await PromotionIntegrationService.DiscountProductAsync(new DiscountProductInputDto(context));

        if (dto.Context.Equals(context))
        {
            return;
        }

        context.CandidateProductDiscounts.Clear();
        context.CandidateProductDiscounts.AddRange(dto.Context.CandidateProductDiscounts);

        context.OrderDiscountPreviews.Clear();
        context.OrderDiscountPreviews.AddRange(dto.Context.OrderDiscountPreviews);
    }
}