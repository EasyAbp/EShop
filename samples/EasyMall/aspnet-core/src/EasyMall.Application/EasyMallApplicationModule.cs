using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using EasyAbp.PaymentService.WeChatPay;
using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallDomainModule),
        typeof(AbpAccountApplicationModule),
        typeof(EasyMallApplicationContractsModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpFeatureManagementApplicationModule),
        typeof(EShopBasketsApplicationModule),
        typeof(EShopOrdersApplicationModule),
        typeof(EShopPaymentsApplicationModule),
        typeof(EShopProductsApplicationModule),
        typeof(EShopStoresApplicationModule),
        typeof(PaymentServiceWeChatPayApplicationModule)
        )]
    public class EasyMallApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EasyMallApplicationModule>();
            });
        }
    }
}
