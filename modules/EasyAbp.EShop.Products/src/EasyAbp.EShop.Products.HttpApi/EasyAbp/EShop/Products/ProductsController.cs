using EasyAbp.EShop.Products.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products
{
    [Area(EShopProductsRemoteServiceConsts.ModuleName)]
    public abstract class ProductsController : AbpControllerBase
    {
        protected ProductsController()
        {
            LocalizationResource = typeof(ProductsResource);
        }
    }
}
