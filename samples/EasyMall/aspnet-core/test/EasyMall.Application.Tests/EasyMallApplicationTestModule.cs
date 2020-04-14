using Volo.Abp.Modularity;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallApplicationModule),
        typeof(EasyMallDomainTestModule)
        )]
    public class EasyMallApplicationTestModule : AbpModule
    {

    }
}