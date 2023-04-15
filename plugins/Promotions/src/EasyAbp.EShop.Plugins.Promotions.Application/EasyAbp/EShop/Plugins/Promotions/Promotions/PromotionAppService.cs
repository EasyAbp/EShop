using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Promotions.Localization;
using EasyAbp.EShop.Plugins.Promotions.Options;
using EasyAbp.EShop.Plugins.Promotions.Permissions;
using EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public class PromotionAppService : MultiStoreCrudAppService<Promotion, PromotionDto, Guid, PromotionGetListInput,
    CreatePromotionDto, UpdatePromotionDto>, IPromotionAppService
{
    protected override string GetPolicyName { get; set; } = PromotionsPermissions.Promotion.Default;
    protected override string GetListPolicyName { get; set; } = PromotionsPermissions.Promotion.Default;
    protected override string CreatePolicyName { get; set; } = PromotionsPermissions.Promotion.Create;
    protected override string UpdatePolicyName { get; set; } = PromotionsPermissions.Promotion.Update;
    protected override string DeletePolicyName { get; set; } = PromotionsPermissions.Promotion.Delete;
    protected override string CrossStorePolicyName { get; set; } = PromotionsPermissions.Promotion.CrossStore;

    private readonly IPromotionRepository _repository;

    protected IJsonSerializer JsonSerializer { get; }
    protected PromotionManager PromotionManager { get; }
    protected EShopPluginsPromotionsOptions Options { get; }

    public PromotionAppService(
        IJsonSerializer jsonSerializer,
        IOptions<EShopPluginsPromotionsOptions> options,
        PromotionManager promotionManager,
        IPromotionRepository repository) : base(repository)
    {
        JsonSerializer = jsonSerializer;
        PromotionManager = promotionManager;
        Options = options.Value;
        _repository = repository;

        LocalizationResource = typeof(PromotionsResource);
        ObjectMapperContext = typeof(EShopPluginsPromotionsApplicationModule);
    }

    protected override async Task<IQueryable<Promotion>> CreateFilteredQueryAsync(PromotionGetListInput input)
    {
        // TODO: AbpHelper generated
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(input.StoreId != null, x => x.StoreId == input.StoreId)
            .WhereIf(!input.PromotionType.IsNullOrWhiteSpace(), x => x.PromotionType.Contains(input.PromotionType!))
            .WhereIf(!input.UniqueName.IsNullOrWhiteSpace(), x => x.UniqueName.Contains(input.UniqueName!))
            .WhereIf(!input.DisplayName.IsNullOrWhiteSpace(), x => x.DisplayName.Contains(input.DisplayName!))
            .WhereIf(input.FromTime != null, x => x.FromTime == input.FromTime)
            .WhereIf(input.ToTime != null, x => x.ToTime == input.ToTime)
            .WhereIf(input.Disabled != null, x => x.Disabled == input.Disabled)
            ;
    }

    public virtual async Task<ListResultDto<PromotionTypeDto>> GetPromotionTypesAsync()
    {
        var promotionTypes = Options.PromotionTypes.GetAll();

        var dtos = new List<PromotionTypeDto>();

        foreach (var promotionType in promotionTypes)
        {
            var handler = GetPromotionHandler(promotionType);

            var configurationsTemplate = await handler.CreateConfigurationsObjectOrNullAsync();

            dtos.Add(new PromotionTypeDto(promotionType.Name, L[promotionType.Name],
                configurationsTemplate is null ? "{}" : JsonSerializer.Serialize(configurationsTemplate)));
        }

        return new ListResultDto<PromotionTypeDto>(dtos);
    }

    protected virtual IPromotionHandler GetPromotionHandler(PromotionTypeDefinition definition)
    {
        return (IPromotionHandler)LazyServiceProvider.LazyGetRequiredService(definition.PromotionHandlerType);
    }

    protected override async Task<Promotion> MapToEntityAsync(CreatePromotionDto createInput)
    {
        return await PromotionManager.CreateAsync(createInput.StoreId, createInput.PromotionType,
            createInput.UniqueName, createInput.DisplayName, createInput.Configurations, createInput.FromTime,
            createInput.ToTime, createInput.Disabled, createInput.Priority);
    }

    protected override async Task MapToEntityAsync(UpdatePromotionDto updateInput, Promotion entity)
    {
        await PromotionManager.UpdateAsync(entity, updateInput.DisplayName, updateInput.Configurations,
            updateInput.FromTime, updateInput.ToTime, updateInput.Disabled, updateInput.Priority);
    }
}