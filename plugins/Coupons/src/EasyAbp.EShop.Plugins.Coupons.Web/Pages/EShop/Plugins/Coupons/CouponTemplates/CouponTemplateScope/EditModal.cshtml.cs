using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope.ViewModels;

namespace EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope
{
    public class EditModalModel : CouponsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CouponTemplateId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid CouponTemplateScopeId { get; set; }

        [BindProperty]
        public CreateEditCouponTemplateScopeViewModel ViewModel { get; set; }

        private readonly ICouponTemplateAppService _service;

        public EditModalModel(ICouponTemplateAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(CouponTemplateId);
            
            ViewModel =
                ObjectMapper.Map<CouponTemplateScopeDto, CreateEditCouponTemplateScopeViewModel>(
                    dto.Scopes.First(x => x.Id == CouponTemplateScopeId));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = await _service.GetAsync(CouponTemplateId);

            dto.Scopes.RemoveAll(x => x.Id == CouponTemplateScopeId);

            var updateDto =
                ObjectMapper.Map<CouponTemplateDto, CreateUpdateCouponTemplateDto>(dto);

            updateDto.Scopes.Add(ObjectMapper
                .Map<CreateEditCouponTemplateScopeViewModel, CreateUpdateCouponTemplateScopeDto>(ViewModel));
            
            await _service.UpdateAsync(CouponTemplateId, updateDto);
            
            return NoContent();
        }
    }
}