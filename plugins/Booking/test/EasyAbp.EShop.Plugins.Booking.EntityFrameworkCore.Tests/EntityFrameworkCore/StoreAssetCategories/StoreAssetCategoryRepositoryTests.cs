using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore.StoreAssetCategories
{
    public class StoreAssetCategoryRepositoryTests : BookingEntityFrameworkCoreTestBase
    {
        private readonly IStoreAssetCategoryRepository _storeAssetCategoryRepository;

        public StoreAssetCategoryRepositoryTests()
        {
            _storeAssetCategoryRepository = GetRequiredService<IStoreAssetCategoryRepository>();
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
