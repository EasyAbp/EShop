using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory;

public interface IInventoryGrainProvider
{
    Task<IInventoryGrain> GetAsync(string grainKey);
}