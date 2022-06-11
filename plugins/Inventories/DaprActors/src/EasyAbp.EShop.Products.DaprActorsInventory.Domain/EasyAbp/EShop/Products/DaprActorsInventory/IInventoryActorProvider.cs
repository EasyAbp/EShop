using System.Threading.Tasks;
using Dapr.Actors;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using JetBrains.Annotations;

namespace EasyAbp.EShop.Products.DaprActorsInventory;

public interface IInventoryActorProvider
{
    Task<IInventoryActor> GetAsync(ActorId actorId, [NotNull] string actorType);
}