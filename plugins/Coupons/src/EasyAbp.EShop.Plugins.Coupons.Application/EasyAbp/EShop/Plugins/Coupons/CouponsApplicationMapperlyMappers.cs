using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateToCouponTemplateDtoMapper : MapperBase<CouponTemplate, CouponTemplateDto>
    {
        public override partial CouponTemplateDto Map(CouponTemplate source);

        public override partial void Map(CouponTemplate source, CouponTemplateDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateScopeToCouponTemplateScopeDtoMapper : MapperBase<CouponTemplateScope, CouponTemplateScopeDto>
    {
        public override partial CouponTemplateScopeDto Map(CouponTemplateScope source);

        public override partial void Map(CouponTemplateScope source, CouponTemplateScopeDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponToCouponDtoMapper : MapperBase<Coupon, CouponDto>
    {
        [MapperIgnoreTarget(nameof(CouponDto.CouponTemplate))]
        public override partial CouponDto Map(Coupon source);

        [MapperIgnoreTarget(nameof(CouponDto.CouponTemplate))]
        public override partial void Map(Coupon source, CouponDto destination);
    }
}
