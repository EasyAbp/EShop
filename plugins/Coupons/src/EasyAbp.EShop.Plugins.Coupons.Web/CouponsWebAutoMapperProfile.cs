using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels;
using AutoMapper;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplateScope.ViewModels;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EShop.Plugins.Coupons.Web
{
    public class CouponsWebAutoMapperProfile : Profile
    {
        public CouponsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<CouponTemplateDto, CreateUpdateCouponTemplateDto>(MemberList.Destination);
            CreateMap<CouponTemplateDto, CreateEditCouponTemplateViewModel>();
            CreateMap<CouponTemplateScopeDto, CreateEditCouponTemplateScopeViewModel>();
            CreateMap<CreateEditCouponTemplateViewModel, CreateUpdateCouponTemplateDto>()
                .Ignore(x => x.Scopes);
            CreateMap<CreateEditCouponTemplateScopeViewModel, CreateUpdateCouponTemplateScopeDto>();
            CreateMap<CouponTemplateScopeDto, CreateUpdateCouponTemplateScopeDto>();
            CreateMap<CouponDto, EditCouponViewModel>();
            CreateMap<CreateCouponViewModel, CreateCouponDto>();
            CreateMap<EditCouponViewModel, UpdateCouponDto>();
        }
    }
}
