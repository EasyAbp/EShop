using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Products.DaprActorsInventory.Domain;

public class DaprActorsProductInventoryProviderTests : ProductsDaprActorsInventoryTestBase
{
    [Fact]
    public async Task Should_Get_Inventory()
    {
        var inventoryProvider = ServiceProvider.GetRequiredService<DaprActorsProductInventoryProvider>();

        var inventoryDataModel = await inventoryProvider.GetInventoryDataAsync(new InventoryQueryModel());

        inventoryDataModel.ShouldNotBeNull();
        inventoryDataModel.Inventory.ShouldBe(100);
        inventoryDataModel.Sold.ShouldBe(0);
    }

    [Fact]
    public async Task Should_Change_Inventory()
    {
        var inventoryProvider = ServiceProvider.GetRequiredService<DaprActorsProductInventoryProvider>();

        var result = await inventoryProvider.TryReduceInventoryAsync(new InventoryQueryModel(), 2, true);

        result.ShouldBeTrue();

        var inventoryDataModel = await inventoryProvider.GetInventoryDataAsync(new InventoryQueryModel());

        inventoryDataModel.ShouldNotBeNull();
        inventoryDataModel.Inventory.ShouldBe(98);
        inventoryDataModel.Sold.ShouldBe(2);

        result = await inventoryProvider.TryIncreaseInventoryAsync(new InventoryQueryModel(), 1, true);

        result.ShouldBeTrue();

        inventoryDataModel = await inventoryProvider.GetInventoryDataAsync(new InventoryQueryModel());

        inventoryDataModel.ShouldNotBeNull();
        inventoryDataModel.Inventory.ShouldBe(99);
        inventoryDataModel.Sold.ShouldBe(1);
    }
}