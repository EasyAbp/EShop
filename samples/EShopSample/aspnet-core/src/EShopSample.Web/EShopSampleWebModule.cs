using System;
using System.IO;
using EasyAbp.EShop;
using EasyAbp.EShop.Baskets;
using EasyAbp.EShop.Baskets.Web;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Web;
using EasyAbp.EShop.Payments;
using EasyAbp.EShop.Payments.Web;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Products.Web;
using EasyAbp.EShop.Stores;
using EasyAbp.EShop.Stores.Web;
using EasyAbp.EShop.Web;
using EasyAbp.PaymentService;
using EasyAbp.PaymentService.Web;
using EasyAbp.PaymentService.WeChatPay;
using EasyAbp.PaymentService.WeChatPay.Web;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore;
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
using Swashbuckle.AspNetCore.Swagger;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Web;
using Volo.Abp.TenantManagement.Web;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.VirtualFileSystem;

namespace EShopSample.Web
{
    [DependsOn(
        typeof(EShopSampleHttpApiModule),
        typeof(EShopSampleApplicationModule),
        typeof(EShopSampleEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAutofacModule),
        typeof(AbpIdentityWebModule),
        typeof(AbpAccountWebIdentityServerModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpTenantManagementWebModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(EShopWebModule),
        typeof(PaymentServiceWebModule),
        typeof(PaymentServiceWeChatPayWebModule)
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
            ConfigureAuthentication(context, configuration);
            ConfigureAutoMapper();
            ConfigureVirtualFileSystem(hostingEnvironment);
            ConfigureLocalizationServices();
            ConfigureNavigationServices();
            ConfigureAutoApiControllers();
            ConfigureSwaggerServices(context.Services);
            ConfigureConventionalControllers();
        }

        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(EShopBasketsApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "eShop";
                });
                options.ConventionalControllers.Create(typeof(EShopOrdersApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "eShop";
                });
                options.ConventionalControllers.Create(typeof(EShopPaymentsApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "eShop";
                });
                options.ConventionalControllers.Create(typeof(EShopProductsApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "eShop";
                });
                options.ConventionalControllers.Create(typeof(EShopStoresApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "eShop";
                });
                
                options.ConventionalControllers.Create(typeof(PaymentServiceApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "paymentService";
                });
                options.ConventionalControllers.Create(typeof(PaymentServiceWeChatPayApplicationModule).Assembly, opt =>
                {
                    opt.RootPath = "paymentService";
                });
            });
        }

        private void ConfigureUrls(IConfiguration configuration)
        {
            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        }

        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddAuthentication()
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "EShopSample";
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
                    
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopBasketsDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopBasketsDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopBasketsApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopBasketsApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets.Application"));
                    options.FileSets.ReplaceEmbeddedByPhysical<EShopBasketsWebModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}modules{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets{Path.DirectorySeparatorChar}src{Path.DirectorySeparatorChar}EasyAbp.EShop.Baskets.Web"));
                    
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

                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
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
            services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EShopSample API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference()
                                {
                                    Id = "Bearer",
                                    Type = ReferenceType.SecurityScheme
                                }
                            }, Array.Empty<string>()
                        }
                    });

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                }
            );
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            app.UseCorrelationId();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
            }
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseAbpRequestLocalization();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "EShopSample API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
