using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.WeChatPay;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Volo.Abp.Account;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.TenantManagement;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallApplicationContractsModule),
        typeof(AbpAccountHttpApiModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpPermissionManagementHttpApiModule),
        typeof(AbpTenantManagementHttpApiModule),
        typeof(AbpFeatureManagementHttpApiModule),
        typeof(EShopBasketsHttpApiModule),
        typeof(EShopOrdersHttpApiModule),
        typeof(EShopPaymentsHttpApiModule),
        typeof(EShopPaymentsWeChatPayHttpApiModule),
        typeof(EShopProductsHttpApiModule),
        typeof(EShopStoresHttpApiModule)
        )]
    public class EasyMallHttpApiModule : AbpModule
    {
        
    }
}
