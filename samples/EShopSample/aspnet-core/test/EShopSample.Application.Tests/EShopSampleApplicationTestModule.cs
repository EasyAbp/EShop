using Volo.Abp.Modularity;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleApplicationModule),
        typeof(EShopSampleDomainTestModule)
        )]
    public class EShopSampleApplicationTestModule : AbpModule
    {

    }
}