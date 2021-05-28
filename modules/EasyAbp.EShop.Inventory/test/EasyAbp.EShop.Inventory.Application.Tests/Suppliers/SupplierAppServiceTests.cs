using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    public class SupplierAppServiceTests : InventoryApplicationTestBase
    {
        private readonly ISupplierAppService _supplierAppService;

        public SupplierAppServiceTests()
        {
            _supplierAppService = GetRequiredService<ISupplierAppService>();
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
