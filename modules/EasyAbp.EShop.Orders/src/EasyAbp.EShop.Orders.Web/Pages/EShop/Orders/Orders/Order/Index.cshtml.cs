using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Orders.Web.Pages.EShop.Orders.Orders.Order
{
    public class IndexModel : OrdersPageModel
    {
        private readonly IStoreAppService _storeAppService;

        [BindProperty(SupportsGet = true)]
        public Guid? StoreId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Guid? CustomerUserId { get; set; }
        
        public string StoreName { get; set; }
        
        public string CustomerUserName { get; set; }

        public IndexModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }
        
        public virtual async Task OnGetAsync()
        {
            if (StoreId.HasValue)
            {
                StoreName = (await _storeAppService.GetAsync(StoreId.Value)).Name;
            }

            if (CustomerUserId.HasValue)
            {
                // Todo: get username
            }
        }
    }
}
