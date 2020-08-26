using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments.Web.Pages.EShop.Payments.Refunds.RefundItem
{
    public class IndexModel : PaymentsPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid RefundId { get; set; }
        
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
