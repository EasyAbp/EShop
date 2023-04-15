using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Stores;

[DependsOn(
    typeof(EShopStoresApplicationContractsModule)
)]
public class EShopStoresWebSharedModule : AbpModule
{
}