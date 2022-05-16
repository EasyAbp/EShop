using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory
{
    public class IndexModel : BookingPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
