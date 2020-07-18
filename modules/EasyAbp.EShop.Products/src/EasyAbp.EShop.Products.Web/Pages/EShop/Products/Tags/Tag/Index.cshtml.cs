using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Tags.Tag
{
    public class IndexModel : ProductsPageModel
    {
        private readonly IStoreAppService _storeAppService;

        [BindProperty(SupportsGet = true)]
        public Guid? StoreId { get; set; }

        public string StoreName { get; set; }

        public IndexModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }

        public virtual async Task OnGetAsync()
        {
            //TODO: when StoreId is empty, should get store which belongs to current user 
            //if (!StoreId.HasValue)
            //{
            //    var store = _storeAppService.GetByOwner(CurrentUser.Id);
            //    StoreId = store.Id;
            //}

            //show default store
            if (!StoreId.HasValue)
            {
                var defaultStore = await _storeAppService.GetDefaultAsync();
                StoreId = defaultStore.Id;
            }

            StoreName = (await _storeAppService.GetAsync(StoreId.Value)).Name;
        }
    }
}
