using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.EShop.Plugins.Coupons.EntityFrameworkCore.CouponTemplates
{
    public class CouponTemplateRepositoryTests : CouponsEntityFrameworkCoreTestBase
    {
        private readonly ICouponTemplateRepository _couponTemplateRepository;

        public CouponTemplateRepositoryTests()
        {
            _couponTemplateRepository = GetRequiredService<ICouponTemplateRepository>();
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
