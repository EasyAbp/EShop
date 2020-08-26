using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Payments.Web.Pages.EShop.Payments.Payments.PaymentItem
{
    public class IndexModel : PaymentsPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid PaymentId { get; set; }
        
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
