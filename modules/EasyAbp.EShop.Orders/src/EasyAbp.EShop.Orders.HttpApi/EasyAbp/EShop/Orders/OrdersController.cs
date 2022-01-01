using EasyAbp.EShop.Orders.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Orders
{
    [Area(EShopOrdersRemoteServiceConsts.ModuleName)]
    public abstract class OrdersController : AbpControllerBase
    {
        protected OrdersController()
        {
            LocalizationResource = typeof(OrdersResource);
        }
    }
}
