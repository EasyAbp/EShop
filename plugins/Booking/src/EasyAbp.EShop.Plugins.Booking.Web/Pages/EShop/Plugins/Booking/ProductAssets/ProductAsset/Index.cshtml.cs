using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset
{
    public class IndexModel : BookingPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
