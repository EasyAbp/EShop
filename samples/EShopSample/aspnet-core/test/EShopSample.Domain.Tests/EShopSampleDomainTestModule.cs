using EShopSample.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EShopSample
{
    [DependsOn(
        typeof(EShopSampleEntityFrameworkCoreTestModule)
        )]
    public class EShopSampleDomainTestModule : AbpModule
    {

    }
}