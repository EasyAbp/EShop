using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope
{
    public class IndexModel : CouponsPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid CouponTemplateId { get; set; }
        
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
