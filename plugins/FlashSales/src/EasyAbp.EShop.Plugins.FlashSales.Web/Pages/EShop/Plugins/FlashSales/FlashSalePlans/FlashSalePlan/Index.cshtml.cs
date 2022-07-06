using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.FlashSales.Web.Pages.EShop.Plugins.FlashSales.FlashSalePlans.FlashSalePlan;

public class IndexModel : FlashSalesPageModel
{
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}
