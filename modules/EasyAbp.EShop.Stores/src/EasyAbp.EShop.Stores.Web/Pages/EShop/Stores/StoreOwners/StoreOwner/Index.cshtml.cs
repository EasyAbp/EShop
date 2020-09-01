using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner
{
    public class IndexModel : StoresPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid? StoreId { get; set; }

        public IndexModel()
        {
        }
        
        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
    }
}
