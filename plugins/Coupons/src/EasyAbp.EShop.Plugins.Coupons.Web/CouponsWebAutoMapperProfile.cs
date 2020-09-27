using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.CouponTemplates.CouponTemplate.ViewModels;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.Web.Pages.EShop.Plugins.Coupons.Coupons.Coupon.ViewModels;
using AutoMapper;
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
            CreateMap<CouponTemplateDto, CreateEditCouponTemplateViewModel>();
            CreateMap<CreateEditCouponTemplateViewModel, CreateUpdateCouponTemplateDto>()
                .Ignore(x => x.Scopes);
            CreateMap<CouponDto, EditCouponViewModel>();
            CreateMap<CreateCouponViewModel, CreateCouponDto>();
            CreateMap<EditCouponViewModel, UpdateCouponDto>();
        }
    }
}
