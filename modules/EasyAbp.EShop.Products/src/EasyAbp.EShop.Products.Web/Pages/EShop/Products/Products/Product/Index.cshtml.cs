using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class IndexModel : ProductsPageModel
    {
        private readonly IStoreAppService _storeAppService;

        [BindProperty(SupportsGet = true)]
        public Guid StoreId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Guid? CategoryId { get; set; }
        
        public string StoreName { get; set; }
        
        public IndexModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }
        
        public virtual async Task OnGetAsync()
        {
            StoreName = (await _storeAppService.GetAsync(StoreId)).Name;
        }
    }
}
