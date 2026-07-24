using EasyAbp.EShop.Plugins.Coupons.Coupons;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.EShop.Plugins.Coupons
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponToCouponDataMapper : MapperBase<Coupon, CouponData>
    {
        public override partial CouponData Map(Coupon source);

        public override partial void Map(Coupon source, CouponData destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateToCouponTemplateDataMapper : MapperBase<CouponTemplate, CouponTemplateData>
    {
        public override partial CouponTemplateData Map(CouponTemplate source);

        public override partial void Map(CouponTemplate source, CouponTemplateData destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class CouponTemplateScopeToCouponTemplateScopeDataMapper : MapperBase<CouponTemplateScope, CouponTemplateScopeData>
    {
        public override partial CouponTemplateScopeData Map(CouponTemplateScope source);

        public override partial void Map(CouponTemplateScope source, CouponTemplateScopeData destination);
    }
}
