using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.AspNetCore.Authorization;
using EasyAbp.EShop.Payments.Authorization;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using EasyAbp.EShop.Payments.Localization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.EShop.Payments.Web
{
    public class PaymentsMenuContributor : IMenuContributor
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
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();
            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<PaymentsResource>>();
             
            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Payments.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem("Payments", l["Menu:Payments"], "/Payments/Payment")
                );
            }
            if (await authorizationService.IsGrantedAsync(PaymentsPermissions.Refunds.Default))
            {
                context.Menu.AddItem(
                    new ApplicationMenuItem("Refunds", l["Menu:Refunds"], "/Refunds/Refund")
                );
            }
        }
    }
}
