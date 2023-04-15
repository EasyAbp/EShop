using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Options;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class PromotionIntegrationService : ApplicationService, IPromotionIntegrationService
{
    protected IPromotionRepository PromotionRepository { get; }
    protected EShopPluginsPromotionsOptions Options { get; }

    public PromotionIntegrationService(
        IPromotionRepository promotionRepository,
        IOptions<EShopPluginsPromotionsOptions> options)
    {
        PromotionRepository = promotionRepository;
        Options = options.Value;
    }

    public virtual async Task<DiscountProductOutputDto> DiscountProductAsync(DiscountProductInputDto input)
    {
        var context = input.Context;

        var promotions = await GetUnexpiredPromotionsAsync(context.Product.StoreId, context.Now, true);

        foreach (var promotion in promotions.OrderByDescending(x => x.Priority))
        {
            var promotionHandler = GetPromotionHandler(promotion.PromotionType);

            await promotionHandler.HandleProductAsync(context, promotion);
        }

        return new DiscountProductOutputDto(context);
    }

    public virtual async Task<DiscountOrderOutputDto> DiscountOrderAsync(DiscountOrderInputDto input)
    {
        var context = input.Context;

        var promotions = await GetUnexpiredPromotionsAsync(context.Order.StoreId, context.Now, false);

        foreach (var promotion in promotions.OrderByDescending(x => x.Priority))
        {
            var promotionHandler = GetPromotionHandler(promotion.PromotionType);

            await promotionHandler.HandleOrderAsync(context, promotion);
        }

        return new DiscountOrderOutputDto(context);
    }

    protected virtual async Task<List<Promotion>> GetUnexpiredPromotionsAsync(Guid storeId, DateTime now,
        bool includesNotStarted)
    {
        // skip the expired promotions.
        var queryable = (await PromotionRepository.GetQueryableAsync()).Where(x =>
            x.StoreId == storeId && !x.Disabled && (!x.ToTime.HasValue || now <= x.ToTime));

        queryable.WhereIf(!includesNotStarted, x => !x.FromTime.HasValue || now >= x.FromTime);

        return await AsyncExecuter.ToListAsync(queryable);
    }

    protected virtual IPromotionHandler GetPromotionHandler(string promotionType)
    {
        var definition = Options.PromotionTypes.GetOrNull(promotionType);

        if (definition is null)
        {
            throw new BusinessException(PromotionsErrorCodes.InvalidPromotionType);
        }

        return (IPromotionHandler)LazyServiceProvider.LazyGetRequiredService(definition.PromotionHandlerType);
    }
}