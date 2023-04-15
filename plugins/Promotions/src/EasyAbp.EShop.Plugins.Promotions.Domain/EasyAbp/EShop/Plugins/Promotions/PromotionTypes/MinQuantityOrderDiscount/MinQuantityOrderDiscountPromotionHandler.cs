using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;

public class MinQuantityOrderDiscountPromotionHandler : PromotionHandlerBase, IScopedDependency
{
    public static string MinQuantityOrderDiscountPromotionTypeName { get; set; } = "MinQuantityOrderDiscount";

    public MinQuantityOrderDiscountPromotionHandler(IJsonSerializer jsonSerializer) : base(jsonSerializer)
    {
    }

    public override async Task HandleProductAsync(ProductDiscountContext context, Promotion promotion)
    {
        foreach (var discountModel in GetConfigurations<MinQuantityOrderDiscountConfigurations>(promotion).Discounts)
        {
            if (context.ProductSku.Currency != discountModel.DynamicDiscountAmount.Currency)
            {
                continue;
            }

            if (!discountModel.IsInScope(context.Product.ProductGroupName, context.Product.Id, context.ProductSku.Id))
            {
                continue;
            }

            var newDiscount = new OrderDiscountPreviewInfoModel(PromotionConsts.PromotionEffectGroup,
                PromotionConsts.PromotionDiscountName, promotion.UniqueName, promotion.DisplayName, promotion.FromTime,
                promotion.ToTime, await CreateOrderDiscountPreviewRuleDataAsync(discountModel, context, promotion));

            context.OrderDiscountPreviews.Add(newDiscount);
        }
    }

    protected virtual Task<string?> CreateOrderDiscountPreviewRuleDataAsync(MinQuantityOrderDiscountModel discountModel,
        ProductDiscountContext context, Promotion promotion)
    {
        return Task.FromResult<string?>(null);
    }

    public override Task HandleOrderAsync(OrderDiscountContext context, Promotion promotion)
    {
        foreach (var discountModel in GetConfigurations<MinQuantityOrderDiscountConfigurations>(promotion).Discounts)
        {
            if (context.Order.Currency != discountModel.DynamicDiscountAmount.Currency)
            {
                continue;
            }

            foreach (var orderLine in context.Order.OrderLines)
            {
                if (discountModel.MinQuantity > orderLine.Quantity)
                {
                    continue;
                }

                if (!discountModel.IsInScope(orderLine.ProductGroupName, orderLine.ProductId, orderLine.ProductSkuId))
                {
                    continue;
                }

                var dynamicDiscountAmountModel = new DynamicDiscountAmountModel(
                    discountModel.DynamicDiscountAmount.Currency,
                    discountModel.DynamicDiscountAmount.DiscountAmount * orderLine.Quantity,
                    discountModel.DynamicDiscountAmount.DiscountRate,
                    discountModel.DynamicDiscountAmount.CalculatorName);

                context.CandidateDiscounts.Add(new OrderDiscountInfoModel(new List<Guid> { orderLine.Id },
                    PromotionConsts.PromotionEffectGroup, PromotionConsts.PromotionDiscountName, promotion.UniqueName,
                    promotion.DisplayName, dynamicDiscountAmountModel));
            }
        }

        return Task.CompletedTask;
    }

    public override Task<object?> CreateConfigurationsObjectOrNullAsync()
    {
        var configuration = new MinQuantityOrderDiscountConfigurations();

        configuration.Discounts.Add(new MinQuantityOrderDiscountModel(new List<ProductScopeModel> { new() },
            2, new DynamicDiscountAmountModel("USD", 0m, 0m, null)));

        return Task.FromResult<object?>(configuration);
    }

    public override bool IsConfigurationValid(string configurations)
    {
        return JsonSerializer.Deserialize<MinQuantityOrderDiscountConfigurations>(configurations) is not null;
    }
}