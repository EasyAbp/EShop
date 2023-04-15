using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.EShop.Plugins.Promotions.Localization;
using EasyAbp.EShop.Plugins.Promotions.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.EShop.Plugins.Promotions.Permissions;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Plugins.Promotions.Web;

[DependsOn(
    typeof(EShopPluginsPromotionsApplicationContractsModule),
    typeof(EShopStoresWebSharedModule),
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(AbpAutoMapperModule)
    )]
public class EShopPluginsPromotionsWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(typeof(PromotionsResource), typeof(EShopPluginsPromotionsWebModule).Assembly);
        });

        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopPluginsPromotionsWebModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new PromotionsMenuContributor());
        });

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EShopPluginsPromotionsWebModule>();
        });

        context.Services.AddAutoMapperObjectMapper<EShopPluginsPromotionsWebModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EShopPluginsPromotionsWebModule>(validate: true);
        });

        Configure<RazorPagesOptions>(options =>
        {
                //Configure authorization.
            });
    }
}
