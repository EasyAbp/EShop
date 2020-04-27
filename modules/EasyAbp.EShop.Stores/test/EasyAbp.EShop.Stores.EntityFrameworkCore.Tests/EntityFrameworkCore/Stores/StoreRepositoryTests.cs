using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Stores.EntityFrameworkCore.Stores
{
    public class StoreRepositoryTests : StoresEntityFrameworkCoreTestBase
    {
        private readonly IRepository<Store, Guid> _storeRepository;

        public StoreRepositoryTests()
        {
            _storeRepository = GetRequiredService<IRepository<Store, Guid>>();
        }

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
    }
}
