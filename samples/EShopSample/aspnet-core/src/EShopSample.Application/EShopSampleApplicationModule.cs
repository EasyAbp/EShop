﻿using EasyAbp.EShop;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Prepayment;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(EShopSampleApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(EShopApplicationModule),
        typeof(EShopPluginsBasketsApplicationModule),
        typeof(PaymentServiceApplicationModule),
        typeof(PaymentServiceWeChatPayApplicationModule),
        typeof(PaymentServicePrepaymentApplicationModule)
    )]
    public class EShopSampleApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopSampleApplicationModule>();
            });
        }
    }
}
