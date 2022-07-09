using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Options;
using EasyAbp.EShop.Products.ProductInventories;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products;

public class ProductInventoryProviderResolver : IProductInventoryProviderResolver, ITransientDependency
{
    protected static ConcurrentDictionary<string, Type> NameToProviderTypeMapping { get; } = new();

    protected IServiceProvider ServiceProvider { get; }

    public ProductInventoryProviderResolver(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public virtual Task<bool> ExistProviderAsync(string providerName)
    {
        TryBuildNameToProviderTypeMapping();

        return Task.FromResult(NameToProviderTypeMapping.ContainsKey(providerName));
    }

    public virtual Task<IProductInventoryProvider> GetAsync(Product product)
    {
        if (!product.InventoryProviderName.IsNullOrWhiteSpace())
        {
            return Task.FromResult(GetProviderByName(product.InventoryProviderName));
        }

        var options = ServiceProvider.GetRequiredService<IOptions<EShopProductsOptions>>();
        var productGroupConfiguration = options.Value.Groups.GetConfiguration(product.ProductGroupName);

        if (!productGroupConfiguration.DefaultInventoryProviderName.IsNullOrWhiteSpace())
        {
            return Task.FromResult(GetProviderByName(productGroupConfiguration.DefaultInventoryProviderName));
        }

        return Task.FromResult(GetProviderByName(options.Value.DefaultInventoryProviderName));
    }

    public virtual Task<IProductInventoryProvider> GetAsync([NotNull] string providerName)
    {
        return Task.FromResult(GetProviderByName(providerName));
    }

    protected virtual IProductInventoryProvider GetProviderByName([CanBeNull] string providerName)
    {
        if (providerName.IsNullOrEmpty())
        {
            providerName = DefaultProductInventoryProvider.DefaultProductInventoryProviderName;
        }

        TryBuildNameToProviderTypeMapping();

        var providerType = NameToProviderTypeMapping[providerName!];

        return (IProductInventoryProvider)ServiceProvider.GetService(providerType);
    }

    protected virtual void TryBuildNameToProviderTypeMapping()
    {
        if (!NameToProviderTypeMapping.IsEmpty)
        {
            return;
        }

        var options = ServiceProvider.GetRequiredService<IOptions<EShopProductsOptions>>().Value;

        foreach (var pair in options.InventoryProviders.GetConfigurationsDictionary())
        {
            NameToProviderTypeMapping[pair.Key] = pair.Value.ProviderType;
        }
    }
}