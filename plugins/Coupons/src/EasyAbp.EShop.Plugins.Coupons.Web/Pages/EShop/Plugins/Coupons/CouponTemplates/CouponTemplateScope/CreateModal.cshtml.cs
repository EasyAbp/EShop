using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope.ViewModels;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope
{
    public class CreateModalModel : CouponsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CouponTemplateId { get; set; }
        
        [BindProperty]
        public CreateEditCouponTemplateScopeViewModel ViewModel { get; set; }

        private readonly ICouponTemplateAppService _service;

        public CreateModalModel(ICouponTemplateAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = await _service.GetAsync(CouponTemplateId);

            var updateDto =
                ObjectMapper.Map<CouponTemplateDto, CreateUpdateCouponTemplateDto>(dto);
            
            var createScopeDto =
                ObjectMapper.Map<CreateEditCouponTemplateScopeViewModel, CreateUpdateCouponTemplateScopeDto>(ViewModel);
            
            updateDto.Scopes.Add(createScopeDto);

            await _service.UpdateAsync(CouponTemplateId, updateDto);
            
            return NoContent();
        }
    }
}