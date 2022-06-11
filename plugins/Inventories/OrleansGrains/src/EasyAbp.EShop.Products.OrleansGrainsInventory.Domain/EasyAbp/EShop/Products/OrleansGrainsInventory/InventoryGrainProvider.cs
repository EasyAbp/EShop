using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;
using Orleans;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory;

public class InventoryGrainProvider : IInventoryGrainProvider, ITransientDependency
{
    private readonly IGrainFactory _grainFactory;

    public InventoryGrainProvider(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }
    
    public virtual Task<IInventoryGrain> GetAsync(string grainKey)
    {
        return Task.FromResult(_grainFactory.GetGrain<IInventoryGrain>(grainKey));
    }
}