using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Inventory.Warehouses
{
    public class WarehouseAppServiceTests : InventoryApplicationTestBase
    {
        private readonly IWarehouseAppService _warehouseAppService;

        public WarehouseAppServiceTests()
        {
            _warehouseAppService = GetRequiredService<IWarehouseAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
