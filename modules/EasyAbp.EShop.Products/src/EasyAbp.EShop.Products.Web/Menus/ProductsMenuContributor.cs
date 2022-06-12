using EasyAbp.EShop.Products.Localization;
using EasyAbp.EShop.Products.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Products.Web.Menus
{
    public class ProductsMenuContributor : IMenuContributor
    {
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenu(context);
            }
        }

        private async Task ConfigureMainMenu(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<ProductsResource>();            
            
            //Add main menu items.

            var productManagementMenuItem = new ApplicationMenuItem(ProductsMenus.Prefix, l["Menu:ProductManagement"]);

            if (await context.IsGrantedAsync(ProductsPermissions.Categories.Manage))
            {
                productManagementMenuItem.AddItem(
                    new ApplicationMenuItem(ProductsMenus.Category, l["Menu:Category"], "/EShop/Products/Categories/Category")
                );
            }

            if (await context.IsGrantedAsync(ProductsPermissions.Products.Manage))
            {
                productManagementMenuItem.AddItem(
                    new ApplicationMenuItem(ProductsMenus.Product, l["Menu:Product"], "/EShop/Products/Products/Product")
                );
            }

            if (!productManagementMenuItem.Items.IsNullOrEmpty())
            {
                var eShopMenuItem = context.Menu.Items.GetOrAdd(i => i.Name == ProductsMenus.ModuleGroupPrefix,
                    () => new ApplicationMenuItem(ProductsMenus.ModuleGroupPrefix, l["Menu:EasyAbpEShop"], icon: "fa fa-shopping-bag"));
                
                eShopMenuItem.Items.Add(productManagementMenuItem);
            }
        }
    }
}
