using Volo.Abp.Modularity;

namespace EasyAbp.EShop
{
    [DependsOn(
        typeof(EShopApplicationModule),
        typeof(EShopDomainTestModule)
        )]
    public class EShopApplicationTestModule : AbpModule
    {

    }
}
