using System.Threading.Tasks;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication
{
    public class IndexModel : StoreApprovalPageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
