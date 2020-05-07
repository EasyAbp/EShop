﻿using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Payments
{
    [DependsOn(
        typeof(EShopPaymentsHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class EShopPaymentsConsoleApiClientModule : AbpModule
    {
        
    }
}
