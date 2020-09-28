using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon
{
    public class CreateModalModel : CouponsPageModel
    {
        [BindProperty]
        public CreateCouponViewModel ViewModel { get; set; }

        private readonly ICouponAppService _service;

        public CreateModalModel(ICouponAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateCouponViewModel, CreateCouponDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}