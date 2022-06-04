using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore.GrantedStores
{
    public class GrantedStoreRepositoryTests : BookingEntityFrameworkCoreTestBase
    {
        private readonly IGrantedStoreRepository _grantedStoreRepository;

        public GrantedStoreRepositoryTests()
        {
            _grantedStoreRepository = GetRequiredService<IGrantedStoreRepository>();
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
