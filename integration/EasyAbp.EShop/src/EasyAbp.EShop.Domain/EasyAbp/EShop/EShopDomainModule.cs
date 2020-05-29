using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;
using Volo.Abp.Modularity;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopDomainSharedModule),
        typeof(EShopBasketsDomainModule),
        typeof(EShopOrdersDomainModule),
        typeof(EShopPaymentsDomainModule),
        typeof(EShopProductsDomainModule),
        typeof(EShopStoresDomainModule)
    )]
    public class EShopDomainModule : AbpModule
    {

    }
}
