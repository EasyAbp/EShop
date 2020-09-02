using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Transactions.Transaction
{
    public class IndexModel : StoresPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid StoreId { get; set; }
        
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
