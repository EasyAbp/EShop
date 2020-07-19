using EasyAbp.EShop.Plugins.Web;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using EasyAbp.EShop.Localization;
using EasyAbp.EShop.Orders.Web;
using EasyAbp.EShop.Payments.Web;
using EasyAbp.EShop.Products.Web;
using EasyAbp.EShop.Stores.Web;
using EasyAbp.EShop.Web.Menus;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.EShop.Web
{
    [DependsOn(
        typeof(EShopHttpApiModule),
        typeof(AbpAspNetCoreMvcUiThemeSharedModule),
        typeof(AbpAutoMapperModule),
        typeof(EShopOrdersWebModule),
        typeof(EShopPaymentsWebModule),
        typeof(EShopPluginsWebModule),
        typeof(EShopProductsWebModule),
        typeof(EShopStoresWebModule)
    )]
    public class EShopWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(typeof(EShopResource), typeof(EShopWebModule).Assembly);
            });

            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EShopWebModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EShopMenuContributor());
            });

            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EShopWebModule>();
            });

            context.Services.AddAutoMapperObjectMapper<EShopWebModule>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopWebModule>(validate: true);
            });

            Configure<RazorPagesOptions>(options =>
            {
                //Configure authorization.
            });
        }
    }
}
