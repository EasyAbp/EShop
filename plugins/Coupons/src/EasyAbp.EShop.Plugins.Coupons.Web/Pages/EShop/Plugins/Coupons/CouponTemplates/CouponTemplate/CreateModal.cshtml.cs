using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate
{
    public class CreateModalModel : CouponsPageModel
    {
        [BindProperty]
        public CreateEditCouponTemplateViewModel ViewModel { get; set; }

        private readonly ICouponTemplateAppService _service;

        public CreateModalModel(ICouponTemplateAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditCouponTemplateViewModel, CreateUpdateCouponTemplateDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}