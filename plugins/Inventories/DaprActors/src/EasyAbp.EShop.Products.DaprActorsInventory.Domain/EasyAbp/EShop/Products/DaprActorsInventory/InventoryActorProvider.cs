using System.Threading.Tasks;
using Dapr.Actors;
using Dapr.Actors.Client;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.DaprActorsInventory;

public class InventoryActorProvider : IInventoryActorProvider, ITransientDependency
{
    public static string ActorType { get; set; } = "InventoryActor";

    public virtual Task<IInventoryActor> GetAsync(ActorId actorId)
    {
        return Task.FromResult(ActorProxy.Create<IInventoryActor>(actorId, ActorType));
    }
}