using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Orders
{
    public abstract class OrdersController : AbpController
    {
        protected OrdersController()
        {
            LocalizationResource = typeof(OrdersResource);
        }
    }
}
