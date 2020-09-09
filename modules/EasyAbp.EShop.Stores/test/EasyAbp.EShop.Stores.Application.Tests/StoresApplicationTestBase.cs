using System;
using EasyAbp.EShop.Stores.EntityFrameworkCore;

namespace EasyAbp.EShop.Stores
{
    /* Inherit from this class for your application layer tests.
     * See SampleAppService_Tests for example.
     */
    public abstract class StoresApplicationTestBase : StoresTestBase<EShopStoresApplicationTestModule>
    {
        protected void UsingDbContext(Action<IStoresDbContext> action)
        {
            using (var dbContext = GetRequiredService<IStoresDbContext>())
            {
                action.Invoke(dbContext);
            }
        }
    }
}