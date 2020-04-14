using EasyAbp.EShop.Orders.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Orders
{
    public abstract class OrdersAppService : ApplicationService
    {
        protected OrdersAppService()
        {
            LocalizationResource = typeof(OrdersResource);
            ObjectMapperContext = typeof(EShopOrdersApplicationModule);
        }
    }
}
