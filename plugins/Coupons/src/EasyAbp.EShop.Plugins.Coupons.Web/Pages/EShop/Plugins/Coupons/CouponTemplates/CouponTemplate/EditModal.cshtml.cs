using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate
{
    public class EditModalModel : CouponsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditCouponTemplateViewModel ViewModel { get; set; }

        private readonly ICouponTemplateAppService _service;

        public EditModalModel(ICouponTemplateAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<CouponTemplateDto, CreateEditCouponTemplateViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = await _service.GetAsync(Id);

            var updateDto =
                ObjectMapper.Map<CreateEditCouponTemplateViewModel, CreateUpdateCouponTemplateDto>(ViewModel);

            updateDto.Scopes =
                ObjectMapper.Map<List<CouponTemplateScopeDto>, List<CreateUpdateCouponTemplateScopeDto>>(dto.Scopes);
            
            await _service.UpdateAsync(Id, updateDto);
            
            return NoContent();
        }
    }
}