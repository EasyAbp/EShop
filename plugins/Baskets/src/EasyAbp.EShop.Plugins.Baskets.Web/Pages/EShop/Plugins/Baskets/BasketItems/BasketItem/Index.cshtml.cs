using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem
{
    public class IndexModel : BasketsPageModel
    {
        [BindProperty(SupportsGet = true)]
        public string BasketName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public Guid? UserId { get; set; }
        
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
