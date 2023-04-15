using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes.SimpleProductDiscount;

public class SimpleProductDiscountPromotionHandler : PromotionHandlerBase, IScopedDependency
{
    public static string SimpleProductDiscountPromotionTypeName { get; set; } = "SimpleProductDiscount";

    public SimpleProductDiscountPromotionHandler(IJsonSerializer jsonSerializer) : base(jsonSerializer)
    {
    }

    public override Task HandleProductAsync(ProductDiscountContext context, Promotion promotion)
    {
        foreach (var discountModel in GetConfigurations<SimpleProductDiscountConfigurations>(promotion).Discounts)
        {
            if (context.ProductSku.Currency != discountModel.DynamicDiscountAmount.Currency)
            {
                continue;
            }

            if (!discountModel.IsInScope(context.Product.ProductGroupName, context.Product.Id, context.ProductSku.Id))
            {
                continue;
            }

            var discount = new CandidateProductDiscountInfoModel(PromotionConsts.PromotionEffectGroup,
                PromotionConsts.PromotionDiscountName, promotion.UniqueName, promotion.DisplayName,
                discountModel.DynamicDiscountAmount, promotion.FromTime, promotion.ToTime);

            context.CandidateProductDiscounts.Add(discount);
        }

        return Task.CompletedTask;
    }

    public override Task HandleOrderAsync(OrderDiscountContext context, Promotion promotion)
    {
        return Task.CompletedTask;
    }

    public override Task<object?> CreateConfigurationsObjectOrNullAsync()
    {
        var configuration = new SimpleProductDiscountConfigurations();

        configuration.Discounts.Add(new SimpleProductDiscountModel(new List<ProductScopeModel> { new() },
            new DynamicDiscountAmountModel("USD", 0m, 0m, null)));

        return Task.FromResult<object?>(configuration);
    }

    public override bool IsConfigurationValid(string configurations)
    {
        return JsonSerializer.Deserialize<SimpleProductDiscountConfigurations>(configurations) is not null;
    }
}