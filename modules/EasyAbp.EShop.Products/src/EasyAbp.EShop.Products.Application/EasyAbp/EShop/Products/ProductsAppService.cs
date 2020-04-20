using EasyAbp.EShop.Products.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Products
{
    public abstract class ProductsAppService : ApplicationService
    {
        protected ProductsAppService()
        {
            LocalizationResource = typeof(ProductsResource);
            ObjectMapperContext = typeof(EShopProductsApplicationModule);
        }
    }
}
