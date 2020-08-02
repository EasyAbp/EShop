using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class IndexModel : ProductsPageModel
    {
        private readonly IStoreAppService _storeAppService;
        private readonly IStoreOwnerAppService _storeOwnerAppService;

        [BindProperty(SupportsGet = true)]
        public Guid? StoreId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? CategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? TagId { get; set; }

        public string StoreName { get; set; }

        public IndexModel(IStoreAppService storeAppService,
            IStoreOwnerAppService storeOwnerAppService)
        {
            _storeAppService = storeAppService;
            _storeOwnerAppService = storeOwnerAppService;
        }

        public virtual async Task OnGetAsync()
        {
            //TODO: Need to handle: when StoreId is empty, and current user owns multiple store
            if (!StoreId.HasValue && CurrentUser.Id.HasValue)
            {
                var storeOwners = await _storeOwnerAppService.GetListAsync(new GetStoreOwnerListDto
                {
                    OwnerId = CurrentUser.Id.Value,
                });

                StoreId = storeOwners.Items.FirstOrDefault()?.StoreId;
            }

            if (!StoreId.HasValue)
            {
                var defaultStore = await _storeAppService.GetDefaultAsync();
                StoreId = defaultStore.Id;
            }

            StoreName = (await _storeAppService.GetAsync(StoreId.Value)).Name;
        }
    }
}
