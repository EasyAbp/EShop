using Volo.Abp.Modularity;

namespace EasyAbp.EShop.Inventory
{
    [DependsOn(
        typeof(InventoryApplicationModule),
        typeof(InventoryDomainTestModule)
        )]
    public class InventoryApplicationTestModule : AbpModule
    {

    }
}
