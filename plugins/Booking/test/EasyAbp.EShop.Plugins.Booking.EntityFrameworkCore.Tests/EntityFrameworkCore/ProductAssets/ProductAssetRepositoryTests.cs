using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore.ProductAssets
{
    public class ProductAssetRepositoryTests : BookingEntityFrameworkCoreTestBase
    {
        private readonly IProductAssetRepository _productAssetRepository;

        public ProductAssetRepositoryTests()
        {
            _productAssetRepository = GetRequiredService<IProductAssetRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
