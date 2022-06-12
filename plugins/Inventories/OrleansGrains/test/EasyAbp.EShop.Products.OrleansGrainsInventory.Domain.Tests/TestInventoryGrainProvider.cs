using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory.Domain;

[Dependency(ReplaceServices = true)]
public class TestInventoryGrainProvider : IInventoryGrainProvider, ITransientDependency
{
    private IInventoryGrain Grain { get; set; }

    private readonly IServiceProvider _serviceProvider;

    public TestInventoryGrainProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task<IInventoryGrain> GetAsync(string grainKey)
    {
        return Task.FromResult(Grain ??= _serviceProvider.GetRequiredService<FakeInventoryGrain>());
    }
}