using System;
using System.Threading.Tasks;
using Dapr.Actors;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.DaprActorsInventory.Domain;

[Dependency(ReplaceServices = true)]
public class TestInventoryActorProvider : IInventoryActorProvider, ITransientDependency
{
    private IInventoryActor Actor { get; set; }

    private readonly IServiceProvider _serviceProvider;

    public TestInventoryActorProvider(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public Task<IInventoryActor> GetAsync(ActorId actorId, string actorType)
    {
        return Task.FromResult(Actor ??= _serviceProvider.GetRequiredService<FakeInventoryActor>());
    }
}