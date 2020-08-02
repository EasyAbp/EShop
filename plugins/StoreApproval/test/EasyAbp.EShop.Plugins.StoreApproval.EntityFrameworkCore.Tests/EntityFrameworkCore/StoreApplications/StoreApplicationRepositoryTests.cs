using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.StoreApproval.EntityFrameworkCore.StoreApplications
{
    public class StoreApplicationRepositoryTests : StoreApprovalEntityFrameworkCoreTestBase
    {
        private readonly IRepository<StoreApplication, Guid> _storeApplicationRepository;

        public StoreApplicationRepositoryTests()
        {
            _storeApplicationRepository = GetRequiredService<IRepository<StoreApplication, Guid>>();
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
