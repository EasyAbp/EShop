using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore.ProductAssetCategories
{
    public class ProductAssetCategoryRepositoryTests : BookingEntityFrameworkCoreTestBase
    {
        private readonly IProductAssetCategoryRepository _productAssetCategoryRepository;

        public ProductAssetCategoryRepositoryTests()
        {
            _productAssetCategoryRepository = GetRequiredService<IProductAssetCategoryRepository>();
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
