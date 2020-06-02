using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments.Web.Pages.EShop.Payments.Refunds.Refund
{
    public class IndexModel : PaymentsPageModel
    {
        private readonly IStoreAppService _storeAppService;

        [BindProperty(SupportsGet = true)]
        public Guid? StoreId { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Guid? UserId { get; set; }
        
        public string StoreName { get; set; }
        
        public string UserName { get; set; }

        public IndexModel(IStoreAppService storeAppService)
        {
            _storeAppService = storeAppService;
        }
        
        public async Task OnGetAsync()
        {
            if (StoreId.HasValue)
            {
                StoreName = (await _storeAppService.GetAsync(StoreId.Value)).Name;
            }

            if (UserId.HasValue)
            {
                // Todo: get username
            }
        }
    }
}
