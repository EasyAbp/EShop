using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.Json;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public abstract class PromotionHandlerBase : IPromotionHandler
{
    protected ConcurrentDictionary<string, object?> ConfigurationCaches { get; } = new();

    protected IJsonSerializer JsonSerializer { get; }

    public PromotionHandlerBase(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public abstract Task HandleProductAsync(ProductDiscountContext context, Promotion promotion);

    public abstract Task HandleOrderAsync(OrderDiscountContext context, Promotion promotion);

    public abstract Task<object?> CreateConfigurationsObjectOrNullAsync();

    public abstract bool IsConfigurationValid(string configurations);

    protected virtual TConfigurations GetConfigurations<TConfigurations>(Promotion promotion)
    {
        if (promotion.Configurations.IsNullOrWhiteSpace())
        {
            return CreateEmptyConfigurations<TConfigurations>();
        }

        var cachedConfigurations = ConfigurationCaches.GetOrAdd(promotion.Configurations,
            JsonSerializer.Deserialize<TConfigurations>(promotion.Configurations));

        return cachedConfigurations is not null
            ? (TConfigurations)cachedConfigurations
            : CreateEmptyConfigurations<TConfigurations>();
    }

    protected virtual TConfigurations CreateEmptyConfigurations<TConfigurations>()
    {
        return JsonSerializer.Deserialize<TConfigurations>("{}");
    }
}