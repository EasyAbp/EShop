using EasyAbp.EShop.Products.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products
{
    public abstract class ProductsController : AbpController
    {
        protected ProductsController()
        {
            LocalizationResource = typeof(ProductsResource);
        }
    }
}
