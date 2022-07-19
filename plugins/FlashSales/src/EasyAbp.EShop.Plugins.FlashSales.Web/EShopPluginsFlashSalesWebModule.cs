using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.EShop.Plugins.FlashSales.Localization;
using EasyAbp.EShop.Plugins.FlashSales.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.EShop.Plugins.FlashSales.Permissions;

namespace EasyAbp.EShop.Plugins.FlashSales.Web;

[DependsOn(
    typeof(EShopPluginsFlashSalesApplicationContractsModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class EShopPluginsFlashSalesWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(FlashSalesResource), typeof(EShopPluginsFlashSalesWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPluginsFlashSalesWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new FlashSalesMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsFlashSalesWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<EShopPluginsFlashSalesWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopPluginsFlashSalesWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
        });
    }
}
