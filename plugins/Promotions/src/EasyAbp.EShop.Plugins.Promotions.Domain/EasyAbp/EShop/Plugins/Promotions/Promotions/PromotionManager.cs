using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Options;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class PromotionManager : DomainService
{
    protected IPromotionRepository Repository => LazyServiceProvider.LazyGetRequiredService<IPromotionRepository>();

    protected EShopPluginsPromotionsOptions Options =>
        LazyServiceProvider.LazyGetRequiredService<IOptions<EShopPluginsPromotionsOptions>>().Value;

    [UnitOfWork]
    public virtual async Task<Promotion> CreateAsync(Guid storeId, string promotionType, string uniqueName,
        string displayName, string configurations, DateTime? fromTime, DateTime? toTime, bool disabled, int priority)
    {
        if (await Repository.AnyAsync(x => x.StoreId == storeId && x.UniqueName == uniqueName))
        {
            throw new BusinessException(PromotionsErrorCodes.DuplicatePromotionUniqueName);
        }

        return new Promotion(GuidGenerator.Create(), CurrentTenant.Id, storeId, promotionType, uniqueName, displayName,
            VerifyAndMinifyConfigurations(promotionType, configurations), fromTime, toTime, disabled, priority);
    }

    public virtual Task UpdateAsync(Promotion promotion, string displayName, string configurations,
        DateTime? fromTime, DateTime? toTime, bool disabled, int priority)
    {
        promotion.Update(displayName, configurations, fromTime, toTime, disabled, priority);

        return Task.CompletedTask;
    }

    protected virtual string VerifyAndMinifyConfigurations(string promotionType, string inputConfigurations)
    {
        var promotionTypeDefinition = Options.PromotionTypes.GetOrNull(promotionType);

        if (promotionTypeDefinition is null)
        {
            throw new BusinessException(PromotionsErrorCodes.InvalidPromotionType);
        }

        var handler = GetPromotionHandler(promotionTypeDefinition);

        var formattedJson = JToken.Parse(inputConfigurations).ToString(Formatting.None);

        if (!handler.IsConfigurationValid(formattedJson))
        {
            throw new BusinessException(PromotionsErrorCodes.InvalidPromotionConfigurations);
        }

        return formattedJson;
    }

    protected virtual IPromotionHandler GetPromotionHandler(PromotionTypeDefinition definition)
    {
        return (IPromotionHandler)LazyServiceProvider.LazyGetRequiredService(definition.PromotionHandlerType);
    }
}