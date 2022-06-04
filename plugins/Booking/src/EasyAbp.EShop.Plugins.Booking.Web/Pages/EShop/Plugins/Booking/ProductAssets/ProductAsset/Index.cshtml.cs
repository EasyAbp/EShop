using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset
{
    public class IndexModel : BookingPageModel
    {
        [BindProperty(SupportsGet = true)]
        public ProductAssetListFilterViewModel Filter { get; set; }
        
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
