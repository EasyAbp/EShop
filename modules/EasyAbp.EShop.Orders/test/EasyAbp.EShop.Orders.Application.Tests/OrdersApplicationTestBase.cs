using System;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using EasyAbp.EShop.Products.Products;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace EasyAbp.EShop.Orders
{
    /* Inherit from this class for your application layer tests.
     * See SampleAppService_Tests for example.
     */
    public abstract class OrdersApplicationTestBase : OrdersTestBase<EShopOrdersApplicationTestModule>
    {
        protected void UsingDbContext(Action<IOrdersDbContext> action)
        {
            using (var dbContext = GetRequiredService<IOrdersDbContext>())
            {
                action.Invoke(dbContext);
            }
        }
    }
}