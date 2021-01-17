using System;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class IndexModel : ProductsPageModel
    {
        private readonly IStoreAppService _storeAppService;
        private readonly IStoreOwnerAppService _storeOwnerAppService;

        [BindProperty(SupportsGet = true)]
        public ProductListFilterViewModel Filter { get; set; }

        public string StoreName { get; set; }
        
        public IndexModel(IStoreAppService storeAppService,
            IStoreOwnerAppService storeOwnerAppService)
        {
            _storeAppService = storeAppService;
            _storeOwnerAppService = storeOwnerAppService;
        }

        public virtual async Task OnGetAsync()
        {
            if (Filter.StoreId.HasValue)
            {
                StoreName = (await _storeAppService.GetAsync(Filter.StoreId.Value)).Name;
            }
        }
    }
}
