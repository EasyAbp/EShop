using EasyMall.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyMall
{
    [DependsOn(
        typeof(EasyMallEntityFrameworkCoreTestModule)
        )]
    public class EasyMallDomainTestModule : AbpModule
    {

    }
}