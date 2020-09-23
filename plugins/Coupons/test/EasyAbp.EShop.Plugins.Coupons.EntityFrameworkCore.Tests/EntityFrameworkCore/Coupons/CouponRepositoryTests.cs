using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore.Coupons
{
    public class CouponRepositoryTests : CouponsEntityFrameworkCoreTestBase
    {
        private readonly ICouponRepository _couponRepository;

        public CouponRepositoryTests()
        {
            _couponRepository = GetRequiredService<ICouponRepository>();
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
