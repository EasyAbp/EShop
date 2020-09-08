using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;

namespace EasyAbp.EShop.Products
{
    /* Inherit from this class for your application layer tests.
     * See SampleAppService_Tests for example.
     */
    public abstract class ProductsApplicationTestBase : ProductsTestBase<EShopProductsApplicationTestModule>
    {
        protected void UsingDbContext(Action<IProductsDbContext> action)
        {
            using (var dbContext = GetRequiredService<IProductsDbContext>())
            {
                action.Invoke(dbContext);
            }
        }
    }
}