using System.Threading.Tasks;

namespace EasyAbp.EShop.Payments.Web.Pages.Refunds.Refund
{
    public class IndexModel : PaymentsPageModel
    {
        public async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
