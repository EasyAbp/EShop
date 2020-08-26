﻿using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleDomainSharedModule),
        typeof(AbpAccountApplicationContractsModule),
        typeof(AbpFeatureManagementApplicationContractsModule),
        typeof(AbpIdentityApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(AbpTenantManagementApplicationContractsModule),
        typeof(AbpObjectExtendingModule),
        typeof(EShopApplicationContractsModule),
        typeof(EShopPluginsBasketsApplicationContractsModule),
        typeof(PaymentServiceApplicationContractsModule),
        typeof(PaymentServiceWeChatPayApplicationContractsModule),
        typeof(PaymentServicePrepaymentApplicationContractsModule)
    )]
    public class EShopSampleApplicationContractsModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EShopSampleDtoExtensions.Configure();
        }
    }
}
