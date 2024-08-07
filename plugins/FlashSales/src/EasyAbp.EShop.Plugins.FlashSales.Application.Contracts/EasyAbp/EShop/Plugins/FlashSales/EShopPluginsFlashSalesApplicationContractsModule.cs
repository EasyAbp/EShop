﻿using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.EShop.Plugins.FlashSales;

[DependsOn(
    typeof(EShopPluginsFlashSalesDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class EShopPluginsFlashSalesApplicationContractsModule : AbpModule
{

}
