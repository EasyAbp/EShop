using System;
using System.IO;
using EasyAbp.BookingService.Web;
using EasyAbp.EShop;
using EasyAbp.EShop.Plugins;
using EasyAbp.EShop.Plugins.Web;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Web;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Web;
using EasyAbp.EShop.Plugins.Baskets;
using EasyAbp.EShop.Plugins.Baskets.Web;
using EasyAbp.EShop.Plugins.Booking;
using EasyAbp.EShop.Plugins.Booking.Web;
using EasyAbp.EShop.Plugins.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Web;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Products.Web;
using EasyAbp.EShop.Stores;
using EasyAbp.EShop.Stores.Web;
using EasyAbp.EShop.Web;
using EasyAbp.PaymentService.Prepayment.Web;
using EasyAbp.PaymentService.Web;
using EasyAbp.PaymentService.WeChatPay.Web;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EShopSample.EntityFrameworkCore;
using EShopSample.Localization;
using EShopSample.MultiTenancy;
using EShopSample.Web.Menus;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Web;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.EShop.Plugins.FlashSales.Web;
using EasyAbp.EShop.Plugins.FlashSales;

namespace EShopSample.Web
{
    [DependsOn(
        typeof(EShopSampleHttpApiModule),
        typeof(EShopSampleApplicationModule),
        typeof(EShopSampleEntityFrameworkCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpFeatureManagementWebModule),
        typeof(AbpSettingManagementWebModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(EShopWebModule),
        typeof(EShopPluginsBasketsWebModule),
        typeof(EShopPluginsBookingWebModule),
        typeof(EShopPluginsCouponsWebModule),
        typeof(EShopPluginsFlashSalesWebModule),
        typeof(PaymentServiceWebModule),
        typeof(PaymentServiceWeChatPayWebModule),
        typeof(PaymentServicePrepaymentWebModule),
        typeof(BookingServiceWebModule)
    )]
    public class EShopSampleWebModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
            {
                options.AddAssemblyResource(
                    typeof(EShopSampleResource),
                    typeof(EShopSampleDomainModule).Assembly,
                    typeof(EShopSampleDomainSharedModule).Assembly,
                    typeof(EShopSampleApplicationModule).Assembly,
                    typeof(EShopSampleApplicationContractsModule).Assembly,
                    typeof(EShopSampleWebModule).Assembly
                );
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            ConfigureUrls(configuration);
            ConfigureBundles();
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
        }

        private void ConfigureBundles()
        {
            Configure<AbpBundlingOptions>(options =>
            {
                options.StyleBundles.Configure(
                    LeptonXLiteThemeBundles.Styles.Global,
                    bundle => { bundle.AddFiles("/global-styles.css"); }
                );
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "EShopSample";
                });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAutoMapper()
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<EShopSampleWebModule>();

            });
        }

        private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopSampleDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EShopSample.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopSampleDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EShopSample.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopSampleApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EShopSample.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopSampleApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}EShopSample.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopSampleWebModule>(hostingEnvironment.ContentRootPath);
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}integration{Path.DirectorySeparatorChar}EasyAbp.EShop{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}integration{Path.DirectorySeparatorChar}EasyAbp.EShop{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}integration{Path.DirectorySeparatorChar}EasyAbp.EShop{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}integration{Path.DirectorySeparatorChar}EasyAbp.EShop{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}integration{Path.DirectorySeparatorChar}EasyAbp.EShop{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopOrdersDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopOrdersDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopOrdersApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopOrdersApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopOrdersWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Orders.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPaymentsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPaymentsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPaymentsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPaymentsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPaymentsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Payments.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopProductsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Products{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Products.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopProductsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Products{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Products.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopProductsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Products{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Products.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopProductsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Products{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Products.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopProductsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Products{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Products.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopStoresDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopStoresDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopStoresApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopStoresApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopStoresWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Stores.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBasketsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Baskets.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBasketsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Baskets.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBasketsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Baskets.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBasketsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Baskets.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBasketsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Baskets.Web"));
                                        
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBookingDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Booking{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Booking.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBookingDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Booking{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Booking.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBookingApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Booking{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Booking.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBookingApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Booking{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Booking.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsBookingWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Booking{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Booking.Web"));
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsCouponsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Coupons{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Coupons.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsCouponsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Coupons{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Coupons.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsCouponsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Coupons{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Coupons.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsCouponsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Coupons{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Coupons.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsCouponsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}Coupons{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.Coupons.Web"));

                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsFlashSalesDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}FlashSales{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.FlashSales.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsFlashSalesDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}FlashSales{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.FlashSales.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsFlashSalesApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}FlashSales{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.FlashSales.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsFlashSalesApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}FlashSales{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.FlashSales.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopPluginsFlashSalesWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}plugins{Path.DirectorySeparatorChar}FlashSales{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Plugins.FlashSales.Web"));
                });
            }
        }

        private void ConfigureLocalizationServices()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EShopSampleResource>()
                    .AddBaseTypes(
                        typeof(AbpUiResource)
                    );

                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        private void ConfigureNavigationServices()
        {
            Configure<AbpNavigationOptions>(options =>
            {
                options.MenuContributors.Add(new EShopSampleMenuContributor());
            });
        }

        private void ConfigureAutoApiControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(EShopSampleApplicationModule).Assembly);
            });
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EShopSample API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseUnitOfWork();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EShopSample API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
