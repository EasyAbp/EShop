using System.Collections.Generic;
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

    public virtual async Task DiscountAsync(GetProductsRealTimePriceContext context)
    {
        if (context.Models.IsNullOrEmpty())
        {
            return;
        }

        var dto = await PromotionIntegrationService.DiscountProductsAsync(new DiscountProductInputDto(context));

        if (dto.Context.Equals(context))
        {
            return;
        }

        foreach (var model in context.Models.Values)
        {
            var targetModel = dto.Context.Models[model.ProductSkuId];

            model.CandidateProductDiscounts.Clear();
            model.CandidateProductDiscounts.AddRange(targetModel.CandidateProductDiscounts);

            model.OrderDiscountPreviews.Clear();
            model.OrderDiscountPreviews.AddRange(targetModel.OrderDiscountPreviews);
        }
    }
}