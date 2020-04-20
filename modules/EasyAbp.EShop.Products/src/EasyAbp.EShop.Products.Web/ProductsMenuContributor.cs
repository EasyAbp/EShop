using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Products.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Products.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Products.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Products.Web
{
    public class ProductsMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<ProductsResource>>();            //Add main menu items.

            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            
            var productManagementMenuItem = new ApplicationMenuItem("ProductManagement", l["Menu:ProductManagement"]);

            if (await authorizationService.IsGrantedAsync(ProductsPermissions.ProductTypes.Default))
            {
                productManagementMenuItem.AddItem(
                    new ApplicationMenuItem("ProductType", l["Menu:ProductType"], "/EShop/Products/ProductTypes/ProductType")
                );
            }

            if (await authorizationService.IsGrantedAsync(ProductsPermissions.Categories.Default))
            {
                productManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Category", l["Menu:Category"], "/EShop/Products/Categories/Category")
                );
            }

            if (await authorizationService.IsGrantedAsync(ProductsPermissions.Products.Default))
            {
                productManagementMenuItem.AddItem(
                    new ApplicationMenuItem("Product", l["Menu:Product"], "/EShop/Products/Products/Product")
                );
            }

            if (!productManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == "EasyAbpEShop",
                    () => new ApplicationMenuItem("EasyAbpEShop", l["Menu:EasyAbpEShop"]));
                
                eShopMenuItem.Items.Add(productManagementMenuItem);
            }
        }
    }
}
