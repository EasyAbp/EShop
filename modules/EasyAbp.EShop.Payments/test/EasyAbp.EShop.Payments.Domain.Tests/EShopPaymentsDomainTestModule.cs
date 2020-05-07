﻿using EasyAbp.EShop.Payments.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EShopPaymentsEntityFrameworkCoreTestModule)
        )]
    public class EShopPaymentsDomainTestModule : AbpModule
    {
        
    }
}
